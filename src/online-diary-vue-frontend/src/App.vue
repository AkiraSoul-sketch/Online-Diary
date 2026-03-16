<script lang="ts">
import { useColorMode } from "@vueuse/core";
import GradesPage from "./modules/GradesContext/GradesPage.vue";

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
