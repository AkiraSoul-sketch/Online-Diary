<script setup lang="ts">
import { cn } from "@/lib/utils";
import type { HTMLAttributes } from "vue/dist/vue.js";
import Label from "../label/Label.vue";

const props = defineProps<{
  class?: HTMLAttributes["class"];
  text: string;
}>();
</script>

<template>
  <h3
    data-slot="card-title"
    :class="cn('leading-none font-semibold card-text', props.class)"
  >
    <div :class="'flex flex-row items-center gap-1 p-1.5'">
      <slot />
      <Label :class="'text-lg'">
        {{ props.text }}
      </Label>
    </div>
  </h3>
</template>
