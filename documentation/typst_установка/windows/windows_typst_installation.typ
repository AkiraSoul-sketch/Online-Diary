#import "presets.typ": *

#heading_1([Гайд по установке Typst для Windows 11.])

_Для работы с Typst потребуется наличие редактора кода Visual Studio Code. Скачать и установить Visual Studio Code можно по ссылке:_ #link("https://code.visualstudio.com/download", text(blue)[скачать Visual Studio Code])

#enum(
    [
        Необходимо скачать архив с последней версией Typst по ссылке: #link("https://typst.app/open-source/#download", text(blue)[скачать])
    ], 
    [
        Создать папку, в которой будет распакован архив, например "C:\\Typst".
        #picture("pictures/typst_folder_example.png", [Пример папки Typst])],
    [
        Скопировать файлы из архива, скачанного из пункта 1 в папку, созданную в пункте 2.
        #picture("pictures/typst_extraction_example.png", [Пример папки Typst])
    ],
    [
        Открыть меню переменных сред окружения в Windows. Чтобы открыть меню переменных сред окружения, можно выполнить `Win + R` (открыть меню выполнить) и ввести `sysdm.cpl`, затем выбрать вкладку _Дополнительно_. Внизу будет кнопка: _«Переменные среды»_: #picture("pictures/system_props.png", "Свойства системы")
    ],
    [
        Скопируйте путь к папке, куда была извлечена программа `typst.exe`. В группе _«Системные переменные»_ выберите _«Path»_ и нажмите _«Изменить»_:
        #picture("pictures/typst_installation_path.png", "Редактирование переменной Path")
    ],
    [
        В открывшемся окне нажмите _«Создать»_, появится новое пустое поле внизу списка, в него нужно вставить скопированный путь к папке с `typst.exe`
        #picture("pictures/typst_path_insert.png", [Вставка пути к папке с typst.exe в переменную Path])
    ],
    [
        Проверьте правильность добавления пути в переменную среду _Path_. Для этого откройте терминал в windows и введите `typst`, должно быть следующее сообщение (не ошибка):
        #picture("pictures/checking.png", [Проверка установки Typst в Windows])
    ],  
    [
        Дальше можно использовать Typst для создания документов. Для этого нужно создать новый файл с расширением `.typ` и ввести в него код для генерации документа. Например, можно использовать следующий код для создания простого документа:

        #par([])

        *Чтобы можно было удобно работать с Typst в Visual Studio Code рекомендуется установить расширение _Tinymist Typst_:*
        #picture("pictures/typst_plugin_vscode.png", [Расширение Tinymist Typst для Visual Studio Code)])

        #par([])
        
        Для того, чтобы ознакомиться с синтаксисом и возможностями Typst, можно обратиться к официальной документации по ссылке: #link("https://typst.app/docs/", text(blue)[официальная документация Typst])
        или попрактиковаться на туториале (_Tutorial_) по ссылке: #link("https://typst.app/docs/tutorial/", text(blue)[туториал по Typst])
    ],    
    )


