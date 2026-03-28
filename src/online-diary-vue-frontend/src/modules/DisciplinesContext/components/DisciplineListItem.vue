<script lang="ts">
import { Button } from "@/components/ui/button";
import { TableCell, TableRow } from "@/components/ui/table";
import { PencilIcon } from "lucide-vue-next";
import type { Discipline } from "../models/Discipline";

export default {
  props: {
    discipline: {
      type: Object as () => Discipline,
      required: true,
    },
  },
  components: {
    TableRow,
    TableCell,
    Button,
    PencilIcon,
  },
  setup() {
    const cellStyle: string =
      "py-2 px-0 flex flex-4 shrink justify-center items-center";
    const rowStyle: string = "flex flex-row flex-wrap justify-around";
    return {
      cellStyle,
      rowStyle,
    };
  },
};
</script>

<template>
  <TableRow :class="rowStyle">
    <TableCell :class="cellStyle">{{ discipline.name }}</TableCell>
    <TableCell :class="cellStyle">{{ discipline.group }}</TableCell>
    <TableCell :class="cellStyle">{{ discipline.teacher }}</TableCell>
    <TableCell :class="cellStyle">{{ discipline.semester }}</TableCell>
    <TableCell :class="cellStyle">
      <Button
        :class="'cursor-pointer rounded-3xl w-6 h-6'"
        :variant="'outline'"
      >
        <PencilIcon :size="15" />
      </Button>
    </TableCell>
  </TableRow>
</template>
