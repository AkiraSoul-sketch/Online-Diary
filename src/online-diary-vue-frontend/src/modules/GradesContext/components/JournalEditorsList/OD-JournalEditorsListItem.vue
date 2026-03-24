<script lang="ts">
import { TableCell, TableRow } from "@/components/ui/table";

export default {
  props: {
    editorName: {
      type: String,
      required: true,
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
    <TableCell> Иванов Иван </TableCell>
  </TableRow>
</template>
