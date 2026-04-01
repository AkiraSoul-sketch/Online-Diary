<script setup lang="ts">
import { useGradebookLogic } from "../gradebook.logic";
import type { StudentInfo } from "../gradebook.models";

const { resolveGradebookColor } = useGradebookLogic();

const props = defineProps<{
  student: StudentInfo;
}>();
</script>

<template>
  <div
    v-for="grade of student.grades"
    :key="student.id + '-' + grade.theme"
    :class="'mx-1 my-1 p-1 drop-shadow-xl justify-center items-center text-center bg-zinc-100'"
  >
    <div :style="{ backgroundColor: resolveGradebookColor(grade) }">
      {{ grade.gradeValue }}
    </div>
  </div>
</template>
