<script setup lang="ts">
import TableCell from "@/components/ui/table/TableCell.vue";

const props = defineProps<{
  cellValue: string | number | undefined;
}>();
</script>

<template>
  <TableCell :class="'text-responsive text-center leading-6'">
    <slot />
    {{ props.cellValue }}
  </TableCell>
</template>
