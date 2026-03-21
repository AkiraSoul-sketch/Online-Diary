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
    resolveGradeColorClass(grade: number): string | undefined {
      switch (grade) {
        case 2:
          return "rgba(239, 68, 68, 0.4)";
        case 3:
          return "rgba(252, 211, 77, 0.4)";
        case 4:
          return "rgba(163, 230, 53, 0.4)";
        case 5:
          return "rgba(52, 211, 153, 0.4)";
        case 1:
          return "rgba(163, 163, 163, 0.4)";
        default:
          return undefined;
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
      :class="'p-0 m-0 w-full flex justify-center items-center h-full bg-red cc'"
      :style="{
        width: `${cellWidth}px`,
        height: `${cellsHeight}px`,
        backgroundColor: resolveGradeColorClass(grade),
      }"
    >
      {{ resolveGradeDisplayText(grade) }}
    </TableRow>
  </GradesTableCell>
</template>
