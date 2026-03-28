<script setup lang="ts">
import { ButtonGroup } from "@/components/ui/button-group";
import AdminPanelMenuItem from "./AdminPanelMenuItem.vue";
import DropdownMenu from "@/components/ui/dropdown-menu/DropdownMenu.vue";

interface AdminPanelMenuItemProp {
  menuName: string;
  type: "left" | "right" | "item";
  menuItems?: string[];
}

interface AdminPanelMenuProps {
  items?: AdminPanelMenuItemProp[];
}

const props = defineProps<AdminPanelMenuProps>();
</script>
<template>
  <ButtonGroup>
    <DropdownMenu>
      <AdminPanelMenuItem
        v-for="item in props.items"
        :key="item.menuName"
        :menu-name="item.menuName"
        :menu-items="item.menuItems"
        :type="item.type"
      />
    </DropdownMenu>
  </ButtonGroup>
</template>
