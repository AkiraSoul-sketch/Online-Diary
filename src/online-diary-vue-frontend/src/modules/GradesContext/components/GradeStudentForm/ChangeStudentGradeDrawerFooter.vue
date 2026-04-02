<script setup lang="ts">
import { CardFooter } from "@/components/ui/card";
import { useGradebookStore } from "../gradebook.store";

const store = useGradebookStore();
</script>

<template>
  <CardFooter :class="'flex flex-col'">
    <div v-if="store.gradingStudentValue !== null">
      <span>Обучающийся: </span>
      <span>{{ store.gradingStudentValue.student.name }}</span>
    </div>
    <span>Последняя дата изменения: </span>
  </CardFooter>
</template>
