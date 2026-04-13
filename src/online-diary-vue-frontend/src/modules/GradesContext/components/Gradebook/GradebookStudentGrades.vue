<script setup lang="ts">
import { useGradebookLogic } from "../gradebook.logic";
import type { Grade, StudentInfo } from "../gradebook.models";
import { useGradebookStore } from "../gradebook.store";
const { resolveGradebookColor } = useGradebookLogic();

const props = defineProps<{
  student: StudentInfo;
}>();

const store = useGradebookStore();

function pickStudentForGraduation(grade: Grade): void {
  store.pickGradingStudent(grade);
}
</script>

<template>
  <button v-for="grade of props.student.grades" :key="props.student.id + '-' + grade.theme" class="grade-block"
    type="button" v-on:click="pickStudentForGraduation(grade)">
    <span class="grade-block__value" :style="{ backgroundColor: resolveGradebookColor(grade) }">
      {{ grade.gradeValue ?? "•" }}
    </span>
  </button>
</template>

<style scoped lang="css">
.grade-block {
  display: grid;
  place-items: center;
  border: 1px solid var(--panel-border-color);
  border-radius: 0.88rem;
  background: var(--panel-bg-gradient), var(--panel-bg);
  color: var(--panel-text-color);
  cursor: pointer;
  transition:
    transform 0.18s ease,
    background 0.18s ease,
    border-color 0.18s ease;
  box-shadow: var(--shadow-primary);
}

.grade-block:hover {
  transform: translateY(-1px);
  border-color: var(--panel-active-border);
  background: var(--panel-bg-active-gradient), var(--panel-bg-active);
}

.grade-block__value {
  display: grid;
  min-height: calc(100% - 0.75rem);
  min-width: calc(100% - 0.75rem);
  place-items: center;
  border-radius: 0.62rem;
  font-size: 0.88rem;
  font-weight: 600;
  line-height: 1;
}

.grade-block:focus-visible {
  outline: 2px solid var(--panel-focus-outline);
  outline-offset: 2px;
}
</style>
