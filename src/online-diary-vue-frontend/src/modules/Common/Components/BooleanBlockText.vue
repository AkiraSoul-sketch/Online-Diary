<script setup lang="ts">

// Компонент для отображения текста, который зависит от булевого значения.
// Особенности:
// 1. Принимает булевое значение и два текста: для true и для false.
// 2. Отображает текст в зависимости от значения: если true - отображает onTrue, если false - отображает onFalse.

const props = defineProps<{
  value: boolean;
  onTrue: string;
  onFalse: string;
}>();
</script>

<template>
  <div>
    {{ props.value ? props.onTrue : props.onFalse }}
  </div>
</template>
