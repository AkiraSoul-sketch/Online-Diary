<script lang="ts">
import GradesPage from "./modules/GradesContext/GradesPage.vue";
import Test from "./modules/GradesContext/Test.vue";

export default {
  components: {
    GradesPage,
    Test,
  },
  setup() {
    // useColorMode();
  },
};
</script>

<template>
  <!-- <ColorsShowcase> </ColorsShowcase> -->
  <GradesPage />
  <Test />
</template>

<style scoped></style>
