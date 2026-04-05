<script setup lang="ts">
import {
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuLabel,
  DropdownMenu,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import type {
  AdminPanelMenuCategory,
  AdminPanelMenuCategoryItem,
} from "./models/admin-panel.models";
import { useAdminStore } from "../../admin.store";
import { classConstructor } from "@/modules/Common/ComponentsLogic/classConstructor";

const props = defineProps<AdminPanelMenuCategory>();
const adminStore = useAdminStore();

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
      <Button :variant="'primary'" :class="classConstructor('text-responsive-secondary', resolveButtonClass(props))">{{
        props.menuName
      }}</Button>
    </DropdownMenuTrigger>
    <DropdownMenuContent v-if="hasMenuItems()">
      <DropdownMenuGroup>
        <RouterLink v-on:click="() => setAdminMenuTitle(item)" v-for="item in props.items" :key="item.menuName"
          :to="item.route">
          <DropdownMenuLabel>{{ item.menuName }}</DropdownMenuLabel>
        </RouterLink>
      </DropdownMenuGroup>
    </DropdownMenuContent>
  </DropdownMenu>
</template>
