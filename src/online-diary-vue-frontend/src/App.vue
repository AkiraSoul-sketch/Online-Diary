<script lang="ts">
import { useColorMode } from "@vueuse/core";
import GradesPage from "./modules/GradesContext/GradesPage.vue";
import Test from "./modules/GradesContext/Test.vue";

export default {
  components: {
    GradesPage,
    Test,
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
