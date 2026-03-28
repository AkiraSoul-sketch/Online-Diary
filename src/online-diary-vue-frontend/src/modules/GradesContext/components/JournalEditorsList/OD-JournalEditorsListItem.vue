<script lang="ts">
import { TableCell, TableRow } from "@/components/ui/table";

export default {
  props: {
    editorName: {
      type: String,
      required: true,
    },
    spacing: {
      type: String,
      required: false,
      default: "p-px",
    },
  },
  components: {
    TableRow,
    TableCell,
  },
};
</script>

<template>
  <TableRow>
    <TableCell :class="spacing"> Иванов Иван </TableCell>
  </TableRow>
</template>
