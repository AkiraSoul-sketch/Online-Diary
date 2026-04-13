<script setup lang="ts">
import { ButtonGroup } from "@/components/ui/button-group";
import AdminPanelMenuItem from "./AdminPanelMenuItem.vue";
import { useAdminStore } from "../../admin.store";

const adminStore = useAdminStore();
</script>
<template>
  <nav class="admin-panel-menu w-full">
    <div class="admin-panel-menu__inner">
      <ButtonGroup :class="'p-1 w-full justify-center'">
        <AdminPanelMenuItem v-for="item in adminStore.$state.categories" :key="item.menuName" v-bind="item" />
      </ButtonGroup>
    </div>
  </nav>
</template>

<style scoped lang="css">
.admin-panel-menu {
  display: block;
  width: 100%;
  padding: 0 clamp(0.75rem, 2vw, 1rem);
}

.admin-panel-menu__inner {
  max-width: 1200px;
  margin: 0 auto;
  background: var(--bg-primary);
  padding: clamp(0.35rem, 1vw, 0.6rem);
  border-radius: 0.5rem;
  box-shadow: var(--shadow-primary);
  display: flex;
  align-items: center;
  justify-content: center;
}

@media (max-width: 720px) {
  .admin-panel-menu__inner {
    padding: 0.25rem;
    border-radius: 0.25rem;
  }
}
</style>
