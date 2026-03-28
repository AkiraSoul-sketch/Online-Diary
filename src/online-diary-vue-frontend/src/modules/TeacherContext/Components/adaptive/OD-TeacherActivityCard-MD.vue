<script setup lang="ts">
import type { Activity } from "../../Models/Activity";

const props = defineProps<Activity>();
</script>

<template>
  <Item
    :variant="'muted'"
    :class="'w-full p-1 border-b gap-0 flex-col flex flex-wrap'"
  >
    <ItemTitle :class="'text-[9px]'">{{ props.name }}</ItemTitle>
    <ItemFooter :class="'text-[8px]'">{{ props.data }}</ItemFooter>
  </Item>
</template>
