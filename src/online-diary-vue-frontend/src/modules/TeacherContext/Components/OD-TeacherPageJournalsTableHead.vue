<script setup lang="ts">
import TableHead from "@/components/ui/table/TableHead.vue";

const props = defineProps<{
  headerValue: string;
}>();
</script>

<template>
  <TableHead
    :class="'text-[var(--text-default)] text-center text-responsive leading-6'"
  >
    {{ headerValue }}
  </TableHead>
</template>
