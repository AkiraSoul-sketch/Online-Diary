<script setup lang="ts">
import {
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuLabel,
  DropdownMenu,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import type { AdminPanelMenuCategory } from "./models/admin-panel.models";

const props = defineProps<AdminPanelMenuCategory>();

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

function hasMenuItems() {
  return props.items && props.items.length > 0;
}
</script>

<template>
  <DropdownMenu>
    <DropdownMenuTrigger :class="'rounded-none'">
      <Button :class="resolveButtonClass(props)">{{ props.menuName }}</Button>
    </DropdownMenuTrigger>
    <DropdownMenuContent v-if="hasMenuItems()">
      <DropdownMenuGroup>
        <RouterLink
          v-for="item in props.items"
          :key="item.menuName"
          :to="item.route"
        >
          <DropdownMenuLabel>{{ item.menuName }}</DropdownMenuLabel>
        </RouterLink>
      </DropdownMenuGroup>
    </DropdownMenuContent>
  </DropdownMenu>
</template>
