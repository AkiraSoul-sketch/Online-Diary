<script setup lang="ts">
import {
  GraduationCap,
  KeyRound,
  LayersIcon,
  Table2Icon,
  UsersIcon,
  FlagIcon,
} from "lucide-vue-next";
import SidebarMenuSectionItem from "./SidebarMenuSectionItem.vue";
import { useSidebarStore } from "../stores/sidebar.store";
import type { SidebarNavigation } from "../models/sidebar.models";
import type { Component } from "vue";

const store = useSidebarStore();

function resolveIcon(item: SidebarNavigation): Component {
  switch (item.name.toLowerCase()) {
    case "преподаватель":
      return GraduationCap;
    case "администрирование":
      return FlagIcon;
    case "журналы":
      return Table2Icon;
    case "участники":
      return UsersIcon;
    case "дисциплины":
      return LayersIcon;
    case "учетные записи":
      return KeyRound;
    default:
      return Table2Icon;
  }
}
</script>

<template>
  <SidebarMenuSectionItem v-for="item in store.navigations" :navigation="item">
    <component :is="resolveIcon(item)" />
  </SidebarMenuSectionItem>
</template>
