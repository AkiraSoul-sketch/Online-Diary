<script setup lang="ts">
import { Button } from "@/components/ui/button";
import { XIcon } from "lucide-vue-next";
import { useCommonStore } from "../../Stores/common.store";

const common = useCommonStore();
</script>

<template>
  <Button
    v-on:click="common.toggleSideBar"
    :class="'border rounded-3xl w-9 shadow-(--shadow-basic)'"
  >
    <XIcon />
  </Button>
</template>
