<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { cn } from "@/lib/utils";
import Label from "../label/Label.vue";

const props = defineProps<{
  class?: HTMLAttributes["class"];
  text: string;
}>();
</script>

<template>
  <div
    data-slot="item-title"
    :class="
      cn(
        'flex w-fit items-center gap-2 text-sm leading-snug font-medium',
        props.class,
      )
    "
  >
    <slot />
    <Label :class="'text-lg'">
      {{ props.text }}
    </Label>
  </div>
</template>
