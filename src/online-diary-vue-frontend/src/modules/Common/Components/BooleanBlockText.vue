<script setup lang="ts">
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
