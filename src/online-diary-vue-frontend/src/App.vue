<script lang="ts">
import GradesPage from "./modules/GradesContext/GradesPage.vue";
import { useColorMode } from "@vueuse/core";

export default {
  components: {
    GradesPage,
  },
  setup() {
    useColorMode();
  },
};
</script>

<template>
  <GradesPage />
</template>

<style scoped></style>
