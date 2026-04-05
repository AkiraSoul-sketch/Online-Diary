<script setup lang="ts">
import type { StudentInfo } from "../gradebook.models";

const props = defineProps<{
  students: StudentInfo[];
}>();

function studentNameText(student: StudentInfo): string {
  const parts = student.name.split(" ");
  return `${parts[0]} ${parts[1][0]}. ${parts[2][0]}.`;
}
</script>

<template>
  <div v-for="student of props.students" :class="'text-responsive-tertiary bg-slate-100 w-full text-center p-1'">
    {{ studentNameText(student) }}
  </div>
</template>
