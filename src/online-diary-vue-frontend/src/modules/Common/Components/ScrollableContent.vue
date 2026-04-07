<script setup lang="ts">
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";

// Компонент для отображения прокручиваемого контента.
// Обертка над стандартным ScrollArea из shadcn.
// Особенности:
// 1. Принимает пропсы widthLimit и heightLimit, которые ограничивают ширину и высоту контента. 
// Если контент превышает эти размеры, появляются полосы прокрутки.

const props = defineProps<{
  widthLimit: number;
  heightLimit: number;
}>();
</script>

<template>
  <ScrollArea :style="{
    width: `${props.widthLimit}px`,
    height: `${props.heightLimit}px`,
  }">
    <slot />
    <ScrollBar :orientation="'vertical'" />
    <ScrollBar :orientation="'horizontal'" />
  </ScrollArea>
</template>
