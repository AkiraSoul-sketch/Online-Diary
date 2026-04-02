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
  <div
    v-for="grade of student.grades"
    :key="student.id + '-' + grade.theme"
    :class="'mx-1 my-1 p-1 drop-shadow-xl justify-center items-center text-center bg-zinc-100'"
    v-on:click="pickStudentForGraduation(grade)"
  >
    <div :style="{ backgroundColor: resolveGradebookColor(grade) }">
      {{ grade.gradeValue }}
    </div>
  </div>
</template>
