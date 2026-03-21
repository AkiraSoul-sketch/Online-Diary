<script lang="ts">
import GradesTableCell from "./GradesTableCell.vue";

interface Student {
  name: string;
  grades: number[];
}

export default {
  components: {
    GradesTableCell,
  },
  props: {
    cellClass: {
      type: String,
      required: false,
    },
    student: {
      type: Object as () => Student,
      required: true,
    },
    textWrapperClasses: {
      type: Array as () => string[],
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
  },
  methods: {
    calculateStudentAverageGrade(student: Student): string {
      if (student.grades.length === 0) return "-";
      const sum = student.grades.reduce((acc, grade) => acc + grade);
      const total = student.grades.length;
      const average = sum / total;
      return average.toFixed(2);
    },
  },
};
</script>

<template>
  <GradesTableCell
    :cell-class="cellClass"
    :text="calculateStudentAverageGrade(student)"
    :text-wrapper-classes="[
      'text-center',
      'flex',
      'justify-center',
      'items-center',
    ]"
    :text-position="'text-center'"
    :cell-width="cellWidth"
  />
</template>
