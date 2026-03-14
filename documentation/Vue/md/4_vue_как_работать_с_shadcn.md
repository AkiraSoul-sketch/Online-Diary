# Как работать/импортировать компоненты shadcn/ui в проекте на Vue

1. _Как импортировать компоненты из shadcn/ui?_

   Для импорта компонентов shadcn/ui в Visual Studio Code должно быть установлено расширение **shadcn/vue**.

   Используя это расширение, вызывайте команду:  
   `>shadcn/vue: Add New Component`

   Выберите нужный компонент.

   Найти все компоненты, которые предоставляет библиотека shadcn/ui, можно в официальной документации: [https://ui.shadcn.com/docs/components](https://ui.shadcn.com/docs/components).

2. В vue-файлах в секции `<template></template>` начинайте набирать нужное название компонента.

   Например, вот импорт компонента `Button` из shadcn/ui:

   ```ts
   import { Button } from "./components/ui/button";
   ```

   Импорт может быть без `{ Component }`, а например: `import Button from "./components/ui/button";`
