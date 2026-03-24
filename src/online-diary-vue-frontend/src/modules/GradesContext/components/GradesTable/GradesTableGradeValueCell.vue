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
          return "--grade-2-bg";
        case 3:
          return "--grade-3-bg";
        case 4:
          return "--grade-4-bg";
        case 5:
          return "--grade-5-bg";
        case 1:
          return "--grade-nan-bg";
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
    :cell-class="cellClass + ' '"
    :text-position="'text-center'"
    :cell-width="cellWidth"
    :show-text="false"
  >
    <TableRow
      :supress-hover-effect="false"
      :class="'p-0 m-0 w-full cursor-pointer flex justify-center items-center h-full'"
      :style="{
        width: `${cellWidth}px`,
        height: `${cellsHeight}px`,
        backgroundColor: `var(${resolveGradeColorClass(grade)})`,
      }"
    >
      {{ resolveGradeDisplayText(grade) }}
    </TableRow>
  </GradesTableCell>
</template>
