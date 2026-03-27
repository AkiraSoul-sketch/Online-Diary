<script lang="ts">
import Avatar from "@/components/ui/avatar/Avatar.vue";
import AvatarFallback from "@/components/ui/avatar/AvatarFallback.vue";
import Item from "@/components/ui/item/Item.vue";
import ItemGroup from "@/components/ui/item/ItemGroup.vue";
import TableRow from "@/components/ui/table/TableRow.vue";
import type { Activity } from "../Models/Activity";

export default {
  components: {
    TableRow,
    ItemGroup,
    Item,
    Avatar,
    AvatarFallback,
  },
  props: {
    activityPred: {
      type: Object as () => Activity,
      required: true,
    },
  },
};
</script>

<template>
  <TableRow :class="'flex justify-between'">
    <ItemGroup :class="'flex flex-row p-2'">
      <div :class="'flex flex-col gap-2'">
        <Item :class="'p-0'">
          <Avatar>
            <AvatarFallback>АА</AvatarFallback>
          </Avatar>
          {{ activityPred.name }}
        </Item>
        <Item :class="'p-0'">
          {{ activityPred.data }}
        </Item>
      </div>
    </ItemGroup>
    <Avatar :class="'mx-5 self-center'">
      <AvatarFallback :class="'bg-red-500'" />
    </Avatar>
  </TableRow>
</template>
