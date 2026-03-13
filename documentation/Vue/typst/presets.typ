#set heading(numbering: "1. ")
#set page(numbering: "1")
#set document(title: "Работа с конфигурацией приложения в .NET проектах")
#set heading(numbering: "1. ")
#set par(first-line-indent: 1.25cm)

#show title: set text(size: 18pt)
#show title: set align(horizon)

// тире
#let tire = "\u{2014}"

// настройка для картинок с Рисунок Х - Название рисунка.
#let picture(image_path, caption) = {
  let normalized_caption = text(caption, size: 11pt)      
  let picture_caption = figure.caption(caption, separator: " " + tire + " ")  
  let picture = figure(image(image_path), caption: picture_caption, supplement: "", numbering: "Рисунок 1")  
  let picture_render = align(center, picture)
  par([])
  picture_render
  par([])
}

#let table_of_contents(title_content) = {
  let title = title(title_content)  
  let outline = outline(title: "Содержание")
  par([])
  title
  par([])
  outline
  par([])
}

#let code_listing(code, caption) = {  
  let border_box = box(        
    inset: 4pt,
    stroke: 1pt + black,
    radius: 1pt,
    code,    
  )      
  let alignment = alignment.center
  let normalized_content = text(caption, size: 11pt)      
  let listing_caption = figure.caption(normalized_content, separator: " " + tire + " ")  
  let listing_caption_content = figure([], caption: listing_caption, numbering: "Листинг 1", supplement: "")
  let listing_caption_render = align(alignment, listing_caption_content)
  let code_listing_render = align(alignment, border_box)
  par([])
  code_listing_render
  listing_caption_render
  par([])
}

#let heading_1(content) = {  
  heading(content, level: 1, numbering: "1. ")
  par([])
  par([])
}

#let heading_2(content) = {
  par([])
  heading(content, level: 2, numbering: "1. ")
  par([])
}

#let numberless_heading(content) = {
  heading(content, numbering: none)
  par([])
}