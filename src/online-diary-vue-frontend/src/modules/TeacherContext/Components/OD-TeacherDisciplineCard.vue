<script setup lang="ts">
import Item from "@/components/ui/item/Item.vue";
import type { Discipline } from "../Models/Discipline";
import ItemTitle from "@/components/ui/item/ItemTitle.vue";
import ItemFooter from "@/components/ui/item/ItemFooter.vue";
import { ItemActions } from "@/components/ui/item";
import Button from "@/components/ui/button/Button.vue";

const props = defineProps<Discipline>();
</script>

<template>
  <Item :variant="'muted'" :class="'p-0 gap-0 bg-block-light-accent'">
    <div :class="'block p-2'">
      <ItemTitle :class="'text-responsive-primary'">{{ props.name }}</ItemTitle>
      <ItemFooter :class="'text-responsive-secondary'">{{
        props.groupname
      }}</ItemFooter>
    </div>
    <ItemActions :class="'block w-full'">
      <Button
        :variant="'header'"
        :class="'h-5 p-0 m-0 w-full text-responsive-secondary'"
        >Перейти</Button
      >
    </ItemActions>
  </Item>
</template>
