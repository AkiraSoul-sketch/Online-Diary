<script setup lang="ts">
import { useDark } from "@vueuse/core";
import GradesPage from "./modules/GradesContext/GradesPage.vue";
import TeacherPage from "./modules/TeacherContext/TeacherPage.vue";

const isDark = useDark();
</script>

<template>
  <TeacherPage/>
</template>

<style scoped></style>
