<script lang="ts">
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Item, ItemContent } from "@/components/ui/item";
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";

export default {
  components: {
    Item,
    ItemContent,
    Avatar,
    AvatarFallback,
    Separator,
    Label,
  },
};
</script>

<template>
  <Item :class="'w-full rounded-none'">
    <ItemContent :class="'flex items-center gap-2'">
      <Avatar :class="'h-18 w-18 border-2 shadow-basic'">
        <AvatarFallback>ПП</AvatarFallback>
      </Avatar>
      <Separator :class="'bg-(--text-main-color)'" />
      <div :class="'flex flex-col items-center'">
        <Label :class="' text-[0.8em]'">Преподаватель П. П.</Label>
        <Label :class="' text-[0.8em]'">Преподаватель</Label>
      </div>
    </ItemContent>
  </Item>
</template>
