#import "presets.typ": *

#heading_1([Как работать/импортировать компоненты shadcn/ui в проекте на Vue])

#enum([
  _Как импортировать компоненты из shadcn/ui?:_ Для импорта компонентов shadcn ui, в Visual Studio Code должно быть установлено расширение:
  #par([])
  *shadcn/vue*.
  #par([])
  Используя это расширение, вызывайте команду: >shadcn/vue: Add New Component

  Выберите нужный компонент.

  Найти все компоненты, которые предоставляет библиотека shadcn/ui, можно в официальной документации: #link("https://ui.shadcn.com/docs/components").  
],
[
  В vue файлах в `<template></template>` секции начинайте набирать нужное название компонента.  

  Например, вот корректный импорт компонента `Button` из shadcn/ui:
  
  #par([])
  ```ts
  import { Button } from "./components/ui/button";
  ```

   Импорт может быть без `{ Component }`, а например: 
   
   `import Button from "./components/ui/button";`
])