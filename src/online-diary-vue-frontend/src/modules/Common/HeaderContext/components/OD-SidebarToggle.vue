<script setup lang="ts">
import { Button } from "@/components/ui/button";
import { MenuIcon } from "lucide-vue-next";

defineProps<{
  sideBarPanelWidth: Number;
}>();
</script>

<template>
  <Button :class="'button-flat-default flex justify-between text-dark'" :variant="'header'" :style="{
    width: `${sideBarPanelWidth}px`,
  }">
    <img src="/main_logo.svg" :class="'h-8 brightness-0'" />
    <MenuIcon :class="'text-dark'" />
  </Button>
</template>
