# Как установить библиотеку компонентов Shadcn для разработки на Vue

Здесь содержится информация о том, как установить библиотеку компонентов shadcn/ui для разработки на Vue.

_Примечание: работать и выполнять команды нужно в корневой папке проекта. Чтобы понять, что вы в корневой папке проекта, проверьте наличие файлов: `package.json`, `tsconfig.json`, `vite.config.ts`_

1. **Tailwind CSS.**  
   Для начала, необходимо установить библиотеку стилей Tailwind CSS, поскольку shadcn/ui использует tailwind css для стилизации компонентов. Это можно сделать выполнив следующую команду:

   ```bash
   npm install tailwindcss @tailwindcss/vite
   ```

   После установки, необходимо создать файл для глобальных стилей: `src/styles/global.css`.  
   Добавьте следующие строки в начало созданного файла глобальных стилей:

   ```css
   @import "tailwindcss";
   ```

   Во Vue файлы стилей необходимо подключать в `main.ts`, чтобы использовать стили в проекте. Откройте файл `src/main.ts` и добавьте следующую строку в секцию импортов для подключения глобальных стилей:

   ```typescript
   import "./styles/global.css";
   ```

   После выполнения вышеуказанных шагов, остаётся последний этап — это подключить Tailwind CSS к сборщику Vite. Для этого откройте файл `vite.config.ts` и добавьте следующий код:

   ```typescript
   import { defineConfig } from "vite";
   import vue from "@vitejs/plugin-vue";
   import tailwindcss from "@tailwindcss/vite"; // импортировать это

   // https://vite.dev/config/
   export default defineConfig({
     plugins: [vue(), tailwindcss()], // здесь добавился tailwindcss() в массив.
   });
   ```

2. **Настройка путей и алиасов.**  
   Далее необходимо настроить пути и алиасы для корректной работы компонентов shadcn/ui. Откройте файл `tsconfig.json`. После секции `references` добавьте следующие строки для настройки путей и алиасов:

   ```json
   "compilerOptions": {
     "baseUrl": ".",
     "paths": {
       "@/*": ["./src/*"]
     }
   }
   ```

   После этого откройте файл `tsconfig.app.json`. Найдите секцию `compilerOptions` и добавьте:

   ```json
   "paths": {
     "@/*": ["./src/*"]
   }
   ```

   Последним этапом является применение этих путей и алиасов в сборщике Vite.  
   Для начала установите библиотеку:

   ```bash
   npm install -D @types/node
   ```

   Вам станет доступен модуль `path` для работы с путями.

   Откройте файл `vite.config.ts` и добавьте следующий код для настройки путей и алиасов:

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

3. **Установка и инициализация shadcn/ui в проекте Vue.**  
   Далее необходимо выполнить следующие команды:

   ```bash
   npx shadcn-vue@latest init
   ```

   Далее вас попросят выбрать предварительные настройки для компонентов shadcn/ui.  
   В рамках разрабатываемого проекта выберите:

   `Slate` в качестве `base color`.

   После завершения установки shadcn-vue, в корне проекта сформируется файл: `components.json`. Там хранятся настройки для компонентов shadcn/ui, которые вы можете использовать в своем проекте.
