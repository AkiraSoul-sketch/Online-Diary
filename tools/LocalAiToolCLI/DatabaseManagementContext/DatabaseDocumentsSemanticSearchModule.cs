using LocalAiToolCLI.EmbeddingContext;
using LocalAiToolCLI.FilesManagementContext;
using LocalAiToolCLI.LoggingManagement;
using SQLitePCL;

namespace LocalAiToolCLI.DatabaseManagementContext;

public static class DatabaseDocumentsSemanticSearchModule
{
    extension(DatabaseInstance instance)
    {
        public LocalFile? SearchRelevantFile(
            EmbeddingModel model,
            string input,
            double threshold = 0.3
        )
        {
            const string sql = """
                SELECT 
                v.rowId,
                v.distance as relevance,
                d.id,
                d.file_path,
                d.file_name
                FROM vector_full_scan(
                    'documents',
                    'embedding',
                    vector_as_f32(?1)
                ) as v
                JOIN documents d ON d.rowId = v.rowId                
                ORDER BY relevance ASC            
                LIMIT 1;
                """;

            instance.InitializeVectorsForTable("documents", "embedding");
            int rc = raw.sqlite3_prepare_v2(instance.Db, sql, out sqlite3_stmt stmt);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }

            float[] vector = model.Generate(input);
            int bytesLength = sizeof(float) * vector.Length;
            byte[] bytes = new byte[bytesLength];
            Buffer.BlockCopy(vector, 0, bytes, 0, bytesLength);
            rc = raw.sqlite3_bind_blob(stmt, 1, bytes);
            if (DatabaseInstance.IsError(rc))
            {
                DatabaseInstance.HandleError(rc);
            }

            try
            {
                rc = raw.sqlite3_step(stmt);
                if (DatabaseInstance.IsError(rc))
                {
                    DatabaseInstance.HandleError(rc);
                }

                if (rc != raw.SQLITE_ROW)
                {
                    return null;
                }

                if (DatabaseInstance.IsError(rc))
                {
                    DatabaseInstance.HandleError(rc);
                }

                double relevance = raw.sqlite3_column_double(stmt, 1);
                Guid id = Guid.Parse(raw.sqlite3_column_text(stmt, 2).utf8_to_string());
                string path = raw.sqlite3_column_text(stmt, 3).utf8_to_string();
                string name = raw.sqlite3_column_text(stmt, 4).utf8_to_string();
                LocalFile file = new()
                {
                    Id = id,
                    Path = path,
                    Name = name,
                };

                string log = $"""
                    {name}
                    {path}
                    имеет релевантность {relevance:0.0000}
                    """;
                log.PrintSuccessMessage();
                return file;
            }
            finally
            {
                raw.sqlite3_finalize(stmt);
                stmt.Dispose();
            }
        }
    }
}
