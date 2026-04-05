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
      :class="'border-0 rounded-none p-1.5 md:p-2.5 lg:p-3.5 xl:p-4.5 cursor-pointer flex justify-start items-center gap-2'">
      <div :class="'cursor-pointer'">
        <slot />
      </div>
      <Label :class="' cursor-pointer text-responsive-secondary'">{{
        navigation.name
      }}</Label>
    </TableRow>
  </RouterLink>
</template>
