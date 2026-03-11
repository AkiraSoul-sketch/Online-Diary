using System.Text;
using System.Text.Json;
using LocalAiToolCLI.DatabaseManagementContext;
using LocalAiToolCLI.EmbeddingContext;
using LocalAiToolCLI.FilesManagementContext;
using LocalAiToolCLI.IOManagementModule;
using LocalAiToolCLI.LoggingManagement;
using LocalAiToolCLI.SettingsManagementContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SQLitePCL;

namespace LocalAiToolCLI.OnStartup;

public static class InitializeDocumentsOnStartupExtension
{
    extension(OnStartupDelegate)
    {
        public static void AddDocumentsTableInitializationOnStartup(IServiceCollection services)
        {
            services.AddSingleton<OnStartupDelegate>(
                (sp) =>
                {
                    using IServiceScope scope = sp.CreateScope();
                    IOptions<DocumentationFolderSettings> docsSettings =
                        scope.ServiceProvider.GetRequiredService<
                            IOptions<DocumentationFolderSettings>
                        >();

                    LocalFile[] files = SearchForDocumentationFiles(docsSettings.Value);
                    if (files.Length == 0)
                    {
                        return;
                    }

                    using DatabaseInstance db =
                        scope.ServiceProvider.GetRequiredService<DatabaseInstance>();
                    LocalFile[] notAdded = GetNotAddedLocalFiles(db, files);
                    if (notAdded.Length > 0)
                    {
                        using EmbeddingModel model =
                            scope.ServiceProvider.GetRequiredService<EmbeddingModel>();
                        "Найдены файлы для добавления в БД. Добавить эти файлы? [Y/y] [N/n]?".PrintWarningMessage();
                        if (Console.UserPressedContinue() == false)
                        {
                            "Отказ от добавления несуществующих файлов в БД.".PrintWarningMessage();
                            return;
                        }

                        AddFiles(files, db, model);
                        "Процесс инициализации базы данных документами завершён.".PrintSuccessMessage();
                    }

                    Console.EmptyLine();
                }
            );
        }

        private static LocalFile[] GetNotAddedLocalFiles(
            DatabaseInstance instance,
            LocalFile[] files
        )
        {
            const string sql = """
                WITH file_paths_json_array AS (
                    SELECT json(?1) AS j
                ),
                file_paths_plain_array AS (
                    SELECT value AS path FROM file_paths_json_array, json_each(j)
                )
                SELECT
                    ppa.path,
                    f.id, 
                    f.file_path, 
                    f.file_name, 
                    f.embedding 
                FROM file_paths_plain_array ppa
                LEFT JOIN documents f ON ppa.path = f.file_path
                WHERE f.id IS NULL
                """;

            string jsonParameter = JsonSerializer.Serialize(files.Select(f => f.Path));
            raw.sqlite3_prepare_v2(instance.Db, sql, out sqlite3_stmt stmt);
            try
            {
                raw.sqlite3_bind_text(stmt, 1, jsonParameter);
                int rc;
                utf8z nullPtr = utf8z.FromIntPtr(IntPtr.Zero);
                List<string> nonExistedPaths = [];
                while ((rc = raw.sqlite3_step(stmt)) == raw.SQLITE_ROW)
                {
                    if (raw.sqlite3_column_type(stmt, 1) == raw.SQLITE_NULL)
                    {
                        continue;
                    }

                    string nonExistingPath = raw.sqlite3_column_text(stmt, 0).utf8_to_string();
                    nonExistedPaths.Add(nonExistingPath);
                }

                if (nonExistedPaths.Count > 0)
                {
                    $"Найдено: {nonExistedPaths.Count} файлов, которых еще нет в базе данных.".PrintWarningMessage();
                    return [.. files.Where(f => nonExistedPaths.Contains(f.Path))];
                }
                else
                {
                    $"Система не нашла файлов, которые еще не были записаны в базу данных".PrintSuccessMessage();
                    return [.. files.IntersectBy(nonExistedPaths, f => f.Path)];
                }
            }
            finally
            {
                raw.sqlite3_finalize(stmt);
                stmt.Dispose();
            }
        }

        private static void AddFiles(
            LocalFile[] files,
            DatabaseInstance database,
            EmbeddingModel model
        )
        {
            "Для корректной работы инструмента, необходимо проинициализировать базу данных вашей документацией.".PrintSystemMessage();
            "Нажмите [Y/y] чтобы продолжить. В противном случае нажмите [N/n].".PrintSystemMessage();
            if (!Console.UserPressedContinue())
            {
                "Отмена инициализации базы данных документацией".PrintWarningMessage();
                return;
            }

            "Начало инициализации базы данных файлами из документации.".PrintSystemMessage();
            using DatabaseTransaction transaction = database.BeginTransaction();
            int batchLimit = 50;
            InsertDocumentLocalFilesByBatch(database, model, batchLimit, files);
            transaction.Commit();
        }

        private static void InsertManyUsingStartBatchIndex(
            DatabaseInstance db,
            EmbeddingModel model,
            int indexStart,
            int indexEnd,
            LocalFile[] files
        )
        {
            var clauses = new List<string>(indexEnd - indexStart);
            int param = 1;
            for (int i = indexStart; i < indexEnd; i++)
            {
                clauses.Add($"(?{param}, ?{param + 1}, ?{param + 2}, vector_as_f32(?{param + 3}))");
                param += 4;
            }

            string sql =
                $"INSERT INTO documents(id, file_path, file_name, embedding) VALUES {string.Join(", ", clauses)}";
            DatabaseQuery query = db.CreateQuery(sql);
            param = 1;
            StringBuilder sb = new();
            try
            {
                for (int i = indexStart; i < indexEnd; i++)
                {
                    string fileName = $"Файл: {files[i].Name}";
                    string contents = files[i].ReadContents();
                    string embeddingsText = sb.AppendLine(fileName).AppendLine(contents).ToString();
                    float[] embeddings = model.Generate(embeddingsText);
                    sb.Clear();

                    query = query
                        .AddGuidParameter(param++, files[i].Id)
                        .AddStringParameter(param++, files[i].Path)
                        .AddStringParameter(param++, files[i].Name)
                        .AddVectorAsBlob(param++, embeddings);
                }

                query.ExecuteAction();
            }
            finally
            {
                query.Dispose();
            }
        }

        private static void InsertDocumentLocalFilesByBatch(
            DatabaseInstance db,
            EmbeddingModel model,
            int batchLimit,
            LocalFile[] files
        )
        {
            db.InitializeVectorsForTable("documents", "embedding");
            for (int i = 0; i < files.Length - 1; i += batchLimit)
            {
                int end = Math.Min(i + batchLimit, files.Length);
                InsertManyUsingStartBatchIndex(db, model, i, end, files);
                PrintProgress(files.Length, end);
            }
        }

        private static void PrintProgress(int total, int done)
        {
            int percent = (int)((double)done / total * 100);
            $"Обработано: {done}|{total}. ({percent}%)".PrintSystemMessage();
        }

        private static LocalFile[] SearchForDocumentationFiles(DocumentationFolderSettings settings)
        {
            "Проверка на существование каких-либо файлов документации".PrintSystemMessage();
            string rootPath = settings.FolderPath;
            string[]? extensions = settings.FileExtensions;
            string[]? ignores = settings.FilesIgnored;
            LocalFile[] files = LocalFile.SearchForDocumentationFiles(
                rootPath,
                extensions,
                ignores
            );
            if (files.Length == 0)
            {
                "Файлов документации не найдено.".PrintWarningMessage();
                return [];
            }

            $"Найдено: {files.Length} файлов".PrintSuccessMessage();
            return files;
        }
    }
}
