<script lang="ts">
import { useDark } from "@vueuse/core";
import ODTeacherPage from "./modules/TeacherContext/OD-TeacherPage.vue";

const a = useDark();

export default {
  components: {
    ODTeacherPage,
  },
};
</script>

<template>
  <OD-TeacherPage />
</template>

<style scoped></style>
