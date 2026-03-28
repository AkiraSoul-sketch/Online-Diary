<script setup lang="ts">
import { ItemFooter, ItemTitle, Item } from "@/components/ui/item";
import type { Activity } from "../Models/Activity";
import Avatar from "@/components/ui/avatar/Avatar.vue";
import AvatarFallback from "@/components/ui/avatar/AvatarFallback.vue";

const props = defineProps<Activity>();
</script>

<template>
  <Item
    :variant="'muted'"
    :class="'w-full p-1 border-b gap-1 flex-col flex flex-wrap items-start'"
  >
    <div :class="'flex gap-2 items-center'">
      <Avatar :class="'border border-amber-950 items-center justify-center'">
        <AvatarFallback :class="'w-6 h-6'">AA</AvatarFallback>
      </Avatar>
      <ItemTitle :class="'text-responsive-tertiary text-wrap'">{{
        props.name
      }}</ItemTitle>
    </div>
    <ItemFooter :class="'text-responsive-tertiary'">{{
      props.data
    }}</ItemFooter>
  </Item>
</template>
