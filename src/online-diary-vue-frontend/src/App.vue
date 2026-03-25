<script lang="ts">
import GradesPage from "./modules/GradesContext/GradesPage.vue";
import DisciplinesPage from "./modules/DisciplinesContext/DisciplinesPage.vue";
import { useColorMode } from "@vueuse/core";

export default {
  components: {
    GradesPage,
    DisciplinesPage,
  },
  setup() {
    // useColorMode();
  },
};
</script>

<template>
  <!-- <GradesPage /> -->
  <DisciplinesPage />
</template>

<style scoped></style>
