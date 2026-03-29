<script setup lang="ts">
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";

const props = defineProps<{
  widthLimit: number;
  heightLimit: number;
}>();
</script>

<template>
  <ScrollArea
    :style="{
      width: `${props.widthLimit}px`,
      height: `${props.heightLimit}px`,
    }"
  >
    <slot />
    <ScrollBar :orientation="'vertical'" />
    <ScrollBar :orientation="'horizontal'" />
  </ScrollArea>
</template>
