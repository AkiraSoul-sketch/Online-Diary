<script setup lang="ts">
import type { Grade } from "../gradebook.models";
import { useGradebookStore } from "../gradebook.store";

const store = useGradebookStore();
function gradeValueText(grade: Grade): string {
  if (grade.gradeValue === null) {
    return "Нет оценки";
  }
  return grade.gradeValue;
}
</script>

<template>
  <div :class="'flex flex-row gap-1'">
    <span>Текущая оценка:</span>
    <span v-if="store.gradingStudentValue !== null" :class="'underline '">{{
      gradeValueText(store.gradingStudentValue.grade)
    }}</span>
  </div>
</template>
