<script setup lang="ts">
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";

const props = defineProps<{
  widthLimit: number;
}>();
</script>

<template>
  <ScrollArea
    :style="{
      width: `${props.widthLimit}px`,
    }"
  >
    <slot />
    <ScrollBar :orientation="'horizontal'" />
  </ScrollArea>
</template>
