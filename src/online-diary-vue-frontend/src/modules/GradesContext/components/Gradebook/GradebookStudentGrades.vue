<script setup lang="ts">
import { useGradebookLogic } from "../gradebook.logic";
import type { Grade } from "../gradebook.models";
import { useGradebookStore } from "../gradebook.store";
const { resolveGradebookColor } = useGradebookLogic();

const props = defineProps<{
  student: any;
}>();

const store = useGradebookStore();

function pickStudentForGraduation(grade: Grade): void {
  store.pickGradingStudent(grade);
}
</script>

<template>
  <div v-for="grade of student.grades" :key="student.id + '-' + grade.theme"
    :class="'mx-1 my-1 p-1 justify-center grade-block  items-center text-center'"
    v-on:click="pickStudentForGraduation(grade)">
    <div :class="'text-responsive-tertiary'" :style="{ backgroundColor: resolveGradebookColor(grade) }">
      {{ grade.gradeValue }}
    </div>
  </div>
</template>

<style lang="css">
.grade-block {
  background-color: var(--bg-primary-accent-2);
  color: var(--fg-primary);
  border: none;
  transition: background 0.2s;
  box-shadow: var(--shadow-primary);
}

.grade-block:hover {
  background-color: var(--bg-primary-accent);
}
</style>
