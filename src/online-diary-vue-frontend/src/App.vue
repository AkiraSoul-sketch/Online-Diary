<script lang="ts">
import { useColorMode } from "@vueuse/core";
import GradesPage from "./modules/GradesContext/GradesPage.vue";
<<<<<<< HEAD
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
=======
import ODAdminPanelSectionsContainer from "./modules/AdministratingContext/OD-AdminPanelSectionsContainer.vue";
import ODAdminPanelSectionItem from "./modules/AdministratingContext/OD-AdminPanelSectionItem.vue";
import ODAdminPanelRecentActionsListContainer from "./modules/AdministratingContext/OD-AdminPanelRecentActionsListContainer.vue";
import ODAdminPanel from "./modules/AdministratingContext/OD-AdminPanel.vue";
>>>>>>> b11c6eb (Сверстана страница админ-панели)
</script>

<template>
  <ODAdminPanel />
</template>

<style scoped></style>
