<script setup lang="ts">
import {
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuLabel,
} from "@/components/ui/dropdown-menu";

import { Button } from "@/components/ui/button";

const props = defineProps<{
  menuName: string;
  type: "left" | "right" | "item";
  menuItems?: string[];
}>();

function resolveButtonClass(type: "left" | "right" | "item"): string {
  switch (type) {
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
  return props.menuItems && props.menuItems.length > 0;
}
</script>

<template>
  <DropdownMenuTrigger :class="'rounded-none'">
    <Button :class="resolveButtonClass(props.type)">{{
      props.menuName
    }}</Button>
  </DropdownMenuTrigger>
  <DropdownMenuContent v-if="hasMenuItems()">
    <DropdownMenuGroup>
      <DropdownMenuLabel v-for="item in props.menuItems" :key="item">{{
        item
      }}</DropdownMenuLabel>
    </DropdownMenuGroup>
  </DropdownMenuContent>
</template>
