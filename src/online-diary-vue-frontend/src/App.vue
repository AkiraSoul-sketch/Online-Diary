<script lang="ts">
import GradesPage from "./modules/GradesContext/GradesPage.vue";
import DisciplinesPage from "./modules/DisciplinesContext/DisciplinesPage.vue";
import { useColorMode } from "@vueuse/core";

export default {
  components: {
    ODSideBar,
    AdminPanel,
    GradesPage,
    DisciplinesPage,
  },
  setup() {
    // useColorMode();
  },
};
import AdminPanel from "./modules/AdministratingContext/AdminPanel.vue";
import ODSideBar from "./modules/SidebarContext/OD-SideBar.vue";
</script>

<template>
  <!-- <GradesPage /> -->
  <DisciplinesPage />
</template>

<style scoped></style>
