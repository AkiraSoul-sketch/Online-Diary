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
  <button
    v-for="grade of props.student.grades"
    :key="props.student.id + '-' + grade.theme"
    class="grade-block"
    type="button"
    v-on:click="pickStudentForGraduation(grade)"
  >
    <span class="grade-block__value" :style="{ backgroundColor: resolveGradebookColor(grade) }">
      {{ grade.gradeValue ?? "•" }}
    </span>
  </button>
</template>

<style scoped lang="css">
.grade-block {
  display: grid;
  place-items: center;
  border: 1px solid hsl(0 0% 100% / 0.05);
  border-radius: 0.88rem;
  background:
    linear-gradient(180deg, hsl(220 12% 25% / 0.94), hsl(220 12% 19% / 0.98)),
    var(--bg-primary-accent-2);
  color: var(--fg-primary);
  cursor: pointer;
  transition:
    transform 0.18s ease,
    background 0.18s ease,
    border-color 0.18s ease;
  box-shadow: var(--shadow-primary);
}

.grade-block:hover {
  transform: translateY(-1px);
  border-color: hsl(102 45% 50% / 0.35);
  background:
    linear-gradient(180deg, hsl(102 24% 24% / 0.96), hsl(220 12% 19% / 0.98)),
    var(--bg-primary-accent);
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
  outline: 2px solid hsl(102 62% 50% / 0.7);
  outline-offset: 2px;
}
</style>
