<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { cn } from "@/lib/utils";

const props = defineProps<{
  class?: HTMLAttributes["class"];
  noWrapper: boolean;
}>();
</script>

<template>
  <div v-if="props.noWrapper" data-slot="table-container" class="relative w-full overflow-auto">
    <table data-slot="table" :class="cn('w-full caption-bottom text-sm', props.class)">
      <slot />
    </table>
  </div>
  <table data-slot="table" :class="cn('w-full caption-bottom text-sm', props.class)" v-if="!props.noWrapper">
    <slot />
  </table>
</template>
