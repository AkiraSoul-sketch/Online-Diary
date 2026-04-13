<script setup lang="ts">
import {
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuLabel,
  DropdownMenu,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { computed } from "vue";
import type {
  AdminPanelMenuCategory,
  AdminPanelMenuCategoryItem,
} from "./models/admin-panel.models";
import { useAdminStore } from "../../admin.store";
import { classConstructor } from "@/modules/Common/ComponentsLogic/classConstructor";

const props = defineProps<AdminPanelMenuCategory>();
const adminStore = useAdminStore();

const isActive = computed(() => adminStore.menuTitle === props.menuName);

const triggerButtonClass = computed(() =>
  classConstructor(
    "admin-panel-trigger text-responsive-secondary px-4 py-2 transition-colors",
    resolveButtonClass(props),
    isActive.value ? "bg-accent text-white shadow-sm" : "bg-transparent"
  ),
);

function resolveButtonClass(category: AdminPanelMenuCategory): string {
  switch (category.type) {
    case "left":
      return "rounded-l-md rounded-r-none border";
    case "right":
      return "rounded-l-none rounded-r-md border-l-0";
    case "item":
      return "rounded-none border-l-0";
    default:
      return "";
  }
}

function setAdminMenuTitle(item: AdminPanelMenuCategoryItem): void {
  adminStore.changeTitle(item.menuName);
}

function hasMenuItems() {
  return props.items && props.items.length > 0;
}
</script>

<template>
  <DropdownMenu>
    <DropdownMenuTrigger :class="'rounded-none'">
      <Button :variant="'primary'" :class="triggerButtonClass">{{ props.menuName }}</Button>
    </DropdownMenuTrigger>
    <DropdownMenuContent v-if="hasMenuItems()">
      <DropdownMenuGroup class="py-1">
        <RouterLink v-for="item in props.items" :key="item.menuName" :to="item.route"
          @click="() => setAdminMenuTitle(item)" :class="'admin-panel-menu__link block px-3 py-2 rounded text-sm'">
          <DropdownMenuLabel>{{ item.menuName }}</DropdownMenuLabel>
        </RouterLink>
      </DropdownMenuGroup>
    </DropdownMenuContent>
  </DropdownMenu>
</template>

<style scoped>
:deep(.admin-panel-trigger):hover {
  background-color: var(--bg-primary-hover) !important;
}

.admin-panel-menu__link:hover {
  background-color: var(--bg-primary-hover);
}
</style>
