<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue";
import CardHeader from "@/components/ui/card/CardHeader.vue";
import { useGradebookStore } from "../gradebook.store";

const store = useGradebookStore();
</script>

<template>
  <CardHeader :class="'flex justify-center gap-10 w-full items-center text-responsive-tertiary'"
    v-if="store.gradingStudentValue !== null">{{ "Тема " + store.gradingStudentValue.theme.date.toLocaleDateString() }}
    <Button :class="'text-responsive-tertiary'" v-on:click="store.disbandGradingStudent()">Отменить</Button>
  </CardHeader>
</template>
