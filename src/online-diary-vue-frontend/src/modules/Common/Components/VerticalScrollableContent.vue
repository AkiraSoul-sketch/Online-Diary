<script setup lang="ts">
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";

// Компонент для отображения вертикально прокручиваемого контента.
// Обертка над стандартным ScrollArea из shadcn.
// Особенности:
// 1. Принимает пропс heightLimit, который ограничивает высоту контента. 
// Если контент превышает эту высоту, появляется вертикальная полоса прокрутки

const props = defineProps<{
  heightLimit: number;
}>();
</script>

<template>
  <ScrollArea :style="{
    height: `${props.heightLimit}px`,
  }">
    <slot />
    <ScrollBar />
  </ScrollArea>
</template>
