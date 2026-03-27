<script lang="ts">
import { Item, ItemFooter, ItemTitle } from "@/components/ui/item";

export default {
  components: {
    Item,
    ItemTitle,
    ItemFooter,
  },
};
</script>

<template>
  <Item :class="'w-full rounded-none'">
    <ItemTitle :class="'text-responsive'">Онлайн журнал</ItemTitle>
    <ItemFooter :class="'text-responsive'"
      >Филиала СибГУ в г.Лесосибирске</ItemFooter
    >
  </Item>
</template>
