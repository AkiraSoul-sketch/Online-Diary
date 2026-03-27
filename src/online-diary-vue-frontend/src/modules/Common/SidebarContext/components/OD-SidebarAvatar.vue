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
      <Avatar
        :class="'border-2 shadow-basic h-14 w-14 md:h-18 md:w-18 lg:h-20 lg:w-20 xl:h-28 xl:w-28'"
      >
        <AvatarFallback>ПП</AvatarFallback>
      </Avatar>
      <Separator :class="'bg-(--text-main-color)'" />
      <div :class="'flex flex-col items-center'">
        <Label :class="' text-responsive'">Преподаватель П. П.</Label>
        <Label :class="' text-responsive'">Преподаватель</Label>
      </div>
    </ItemContent>
  </Item>
</template>
