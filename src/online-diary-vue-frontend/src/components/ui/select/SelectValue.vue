<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { cn } from "@/lib/utils";
import type { SelectValueProps } from "reka-ui";
import { SelectValue } from "reka-ui";

const props = defineProps<
  SelectValueProps & {
    class?: HTMLAttributes["class"];
  }
>();
</script>

<template>
  <SelectValue
    data-slot="select-value"
    v-bind="props"
    :class="cn('text-responsive-secondary font-normal text-foreground/80', props.class)"
  >
    <slot />
  </SelectValue>
</template>
