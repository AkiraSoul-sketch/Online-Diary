<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { cn } from "@/lib/utils";

const props = defineProps<{
  class?: HTMLAttributes["class"];
  supressHoverEffect?: boolean;
}>();
</script>

<template>
  <tr
    data-slot="table-row"
    :class="
      cn(
        'border-b-(--text-muted) text-(--text-muted) data-[state=selected]:bg-muted transition-colors',
        props.supressHoverEffect
          ? ''
          : 'hover:text-(--text-default) hover:bg-(--bg)',
        props.class,
      )
    "
  >
    <slot />
  </tr>
</template>
