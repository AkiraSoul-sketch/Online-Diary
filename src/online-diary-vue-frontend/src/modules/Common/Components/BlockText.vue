<script setup lang="ts">

// компонент для отображения текста с поддержкой placeholder.
// отображает текст в блоке.

const props = defineProps<{
  value?: string | number | Date | null;
  placeholder?: string;
}>();

function resolveText(input: string | number | Date): string {
  if (input instanceof Date) {
    return input.toLocaleDateString();
  }
  return String(input);
}
</script>

<template>
  <div v-if="props.value">
    {{ resolveText(props.value) }}
  </div>
  <div v-else>
    {{ props.placeholder }}
  </div>
</template>
