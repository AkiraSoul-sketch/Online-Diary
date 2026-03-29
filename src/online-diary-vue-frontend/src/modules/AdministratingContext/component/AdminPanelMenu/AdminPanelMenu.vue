<script setup lang="ts">
import { ButtonGroup } from "@/components/ui/button-group";
import AdminPanelMenuItem from "./AdminPanelMenuItem.vue";
import { useAdminStore } from "./admin.store";

const adminStore = useAdminStore();
</script>
<template>
  <ButtonGroup>
    <AdminPanelMenuItem
      v-for="item in adminStore.$state.categories"
      :key="item.menuName"
      v-bind="item"
    />
  </ButtonGroup>
</template>
