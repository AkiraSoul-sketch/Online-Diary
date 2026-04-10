<script setup lang="ts">
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";

// Компонент для отображения горизонтально прокручиваемого контента.
// Обертка над стандартным ScrollArea из shadcn.
// Особенности:
// 1. Принимает пропс widthLimit, который ограничивает ширину контента. 
// Если контент превышает эту ширину, появляется горизонтальная полоса прокрутки.

const props = defineProps<{
  widthLimit: number;
}>();
</script>

<template>
  <ScrollArea :style="{
    width: `${props.widthLimit}px`,
  }">
    <slot />
    <ScrollBar :orientation="'horizontal'" />
  </ScrollArea>
</template>
