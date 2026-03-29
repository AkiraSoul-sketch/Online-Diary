<script setup lang="ts">
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";

const props = defineProps<{
  heightLimit: number;
}>();
</script>

<template>
  <ScrollArea
    :style="{
      height: `${props.heightLimit}px`,
    }"
  >
    <slot />
    <ScrollBar />
  </ScrollArea>
</template>
