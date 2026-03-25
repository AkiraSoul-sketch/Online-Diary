<script lang="ts">
import { useColorMode } from "@vueuse/core";
import GradesPage from "./modules/GradesContext/GradesPage.vue";

export default {
  components: {
    ODSideBar,
    AdminPanel,
    GradesPage,
  },
  setup() {
    useColorMode();
  },
};
import AdminPanel from "./modules/AdministratingContext/AdminPanel.vue";
import ODSideBar from "./modules/SidebarContext/OD-SideBar.vue";
</script>

<template>
  <AdminPanel />
</template>

<style scoped></style>
