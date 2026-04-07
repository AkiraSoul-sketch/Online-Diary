<script setup lang="ts">

// Компонент для отображения текста в одной строке с поддержкой placeholder.
// Отображает текст в span, если значение есть, иначе отображает placeholder.

const props = defineProps<{
  value?: string | number | Date;
  placeholder: string;
}>();

function resolveText(input: string | number | Date): string {
  if (input instanceof Date) {
    return input.toLocaleDateString();
  }
  return String(input);
}
</script>

<template>
  <span v-if="props.value">
    {{ resolveText(props.value) }}
  </span>
  <span v-else>
    {{ props.placeholder }}
  </span>
</template>
