<script setup lang="ts">
import type { ThemeInfo } from "../gradebook.models";

const props = defineProps<{
  theme: ThemeInfo;
}>();
</script>

<template>
  <div
    :class="'z-10  relative text-center [writing-mode:vertical-rl] rotate-220'"
  >
    <span :class="'relative right-10 z-20'">
      {{ "Тема " + theme.date.toLocaleDateString() }}
    </span>
  </div>
</template>
