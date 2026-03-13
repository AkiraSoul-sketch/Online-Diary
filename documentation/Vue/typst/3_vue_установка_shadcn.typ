#import "presets.typ": *

#heading_1([Как установить библиотеку компонентов Shadcn для разработки на Vue])

Здесь содержится информация о том, как установить библиотеку компонентов shadcn/ui для разработки на Vue.

_Примечание: работать и выполнять команды нужно в корневой папке проекта. Чтобы понять, что вы в корневой папке проекта, проверьте наличие файлов: `package.json`, `tsconfig.json`, `vite.config.ts`_

#enum([
  *Tailwind CSS.* Для начала, необходимо установить библиотеку стилей tailwind css, поскольку shadcn/ui использует tailwind css для стилизации компонентов. Это можно сделать выполнив следующую команду.
  #par([])
  ```bash
  npm install tailwindcss @tailwindcss/vite
  ```
  #par([]) 
  После установки, необходимо создать файл для глобальных стилей. Создайте файл для глобальных стилей: `src/styles/global.css`.
  #par([])
  Добавьте следующие строки в начало созданного файла глобальных стилей:
  #par([])
  ```css
  @import "tailwindcss";
  ```
  #par([])

  Во Vue, файлы стилей необходимо подключать в `main.ts`, для того, чтобы использовать стили в проекте. Откройте файл `src/main.ts` и добавьте следующую строку в секции импортов для подключения глобальных стилей:
  #par([])
  ```typescript
  import "./styles/global.css";
  ```
  #par([])
  После выполнения вышеуказанных шагов, остается последний этап #tire это подключить tailwind css к сборщику vite. Для этого откройте файл `vite.config.ts` и добавьте следующий код для подключения tailwind css к сборщику:
  #par([])
  ```typescript
  import { defineConfig } from "vite";
  import vue from "@vitejs/plugin-vue";
  import tailwindcss from "@tailwindcss/vite"; // импортировать это

  // https://vite.dev/config/
  export default defineConfig({
    plugins: [vue(), tailwindcss()], // здесь добавился tailwindcss() в массив.    
  });

  ```
  #colbreak()
],
[
  *Настройка путей и алиасов.* Далее необходимо настроить пути и алиасы для корректной работы компонентов shadcn/ui. Откройте файл `tsconfig.json`. После секции: `references` добавьте следующие строки для настройки путей и алиасов:
  #par([])
  ```json
  "compilerOptions": {
    "baseUrl": ".",
    "paths": {
      "@/*": ["./src/*"]
    }
  }
  ```
  #par([])
  После этого, откройте файл: `tsconfig.app.json`. Найдите секцию `compilerOptions` и добавьте следующие строки для настройки путей и алиасов:
  #par([])
  ```json
    "paths": {
      "@/*": ["./src/*"]
    }
  ```

  Последним этапом является применение этих путей и алиасов в сборщике vite.
  Для начала, установите библиотеку *`npm install -D @types/node`*. Вам станет доступен модуль `path` для работы с путями. 

  Откройте файл `vite.config.ts` и добавьте следующий код для настройки путей и алиасов в сборщике:

  #par([])
  ```typescript
  import { defineConfig } from "vite";
  import vue from "@vitejs/plugin-vue";  
  import path from "node:path"; // импортировать это
  import tailwindcss from "@tailwindcss/vite"; // должно быть еще это

  // https://vite.dev/config/
  export default defineConfig({
    plugins: [vue(), tailwindcss()], // должен быть tailwindcss() в массиве.
    // добавить эту секцию:
    resolve: {
      alias: {
        "@": path.resolve(__dirname, "./src"),
      },
    },
  });
  ```
  #par([])
],
[
  *Установка и инициализация shadcn/ui в проекте Vue.* Далее необходимо выполнить следующие команды:
  #par([])
  ```bash
  npx shadcn-vue@latest init
  ```
  #par([])
  Далее вас попросят выбрать предварительные настройки для компонентов shadcn/ui.
  В рамках разрабатываемого проекта выберите:

  `Slate` в качестве `base color`

  После завершения установки `shadcn-vue` в корне проекта сформируется файл: `components.json`. Там хранятся настройки для компонентов shadcn/ui, которые вы можете использовать в своем проекте.
])