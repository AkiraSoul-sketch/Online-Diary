<script setup lang="ts">
import Label from "@/components/ui/label/Label.vue";
import { TableRow } from "@/components/ui/table";
import type { SidebarNavigation } from "../models/sidebar.models";

defineProps<{
  navigation: SidebarNavigation;
}>();
</script>

<template>
  <RouterLink :to="navigation.link">
    <TableRow
      :class="'border-0 rounded-none p-1.5 cursor-pointer flex justify-start items-center gap-2'"
    >
      <div :class="'cursor-pointer'">
        <slot></slot>
      </div>
      <Label :class="'cursor-pointer  text-[0.7em]'">{{
        navigation.name
      }}</Label>
    </TableRow>
  </RouterLink>
</template>
