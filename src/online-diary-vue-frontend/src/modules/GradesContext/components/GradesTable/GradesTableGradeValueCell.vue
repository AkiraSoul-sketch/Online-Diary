<script lang="ts">
import { TableRow } from "@/components/ui/table";
import GradesTableCell from "./GradesTableCell.vue";
import { Button } from "@/components/ui/button";
import { PenIcon } from "lucide-vue-next";
import { Label } from "@/components/ui/label";

export default {
  components: {
    GradesTableCell,
    TableRow,
    Button,
    PenIcon,
    Label,
  },
  props: {
    cellClass: {
      type: String,
      required: false,
    },
    textPosition: {
      type: String,
      required: false,
    },
    cellWidth: {
      type: Number,
      required: false,
    },
    cellsHeight: {
      type: Number,
      required: false,
    },
    grade: {
      type: Number,
      required: true,
    },
  },
  methods: {
    resolveGradeColorClass(grade: number): string {
      switch (grade) {
        case 5:
          return "bg-[#83b771]";
        case 4:
          return "bg-[#7c7bcb]";
        case 3:
          return "bg-[#b79671]";
        case 2:
          return "bg-[#b16ef0]";
        default:
          return "bg-gray-500";
      }
    },
    resolveGradeDisplayText(grade: number): string {
      switch (grade) {
        case 5:
          return "5";
        case 4:
          return "4";
        case 3:
          return "3";
        case 2:
          return "2";
        case 1:
          return "Н";
        default:
          return "-";
      }
    },
  },
};
</script>

<template>
  <GradesTableCell
    :cell-class="cellClass"
    :text-position="'text-center'"
    :cell-width="cellWidth"
    :show-text="false"
  >
    <TableRow
      :class="'p-0 m-0 w-full flex justify-center items-center h-full'"
      :style="{
        width: `${cellWidth}px`,
        height: `${cellsHeight}px`,
      }"
    >
      <Button
        :class="`text-white ${resolveGradeColorClass(grade)} flex-none p-0 w-6 rounded-xl h-6 flex items-center justify-center`"
      >
        {{ resolveGradeDisplayText(grade) }}
      </Button>
    </TableRow>
  </GradesTableCell>
</template>
