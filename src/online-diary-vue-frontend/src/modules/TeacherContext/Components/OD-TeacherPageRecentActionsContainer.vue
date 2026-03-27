<script lang="ts">
import Item from "@/components/ui/item/Item.vue";
import ScrollArea from "@/components/ui/scroll-area/ScrollArea.vue";
import ScrollBar from "@/components/ui/scroll-area/ScrollBar.vue";
import Table from "@/components/ui/table/Table.vue";
import TableBody from "@/components/ui/table/TableBody.vue";
import ODTeacherPageRecentActionItem from "./OD-TeacherPageRecentActionItem.vue";
import type { Activity } from "../Models/Activity";
import ODTeacherPageJournalTableHeader from "./OD-TeacherPageJournalTableHeader.vue";

function generateAcrivity(count: number): Activity[] {
  const ActivityName =
    "Поставил А. А. Ахматшину оценку 2 по дисциплине: Информатика";

  const Activity = [];
  for (let B = 0; B < count; B++) {
    const activity: Activity = {
      name: ActivityName,
      data: "Час назад",
    };
    Activity.push(activity);
  }
  return Activity;
}

export default {
  components: {
    ScrollArea,
    Item,
    Table,
    TableBody,
    ScrollBar,
    ODTeacherPageRecentActionItem,
    ODTeacherPageJournalTableHeader,
  },
  data() {
    return {
      GenerateAcrivity: generateAcrivity(10),
    };
  },
};
</script>

<template>
  <Item :class="'p-0 gap-0 flex flex-col w-full'" :variant="'muted'">
    <ODTeacherPageJournalTableHeader
      :class="'self-start'"
      :-header-value="'Недавняя активность:'"
    />
    <ScrollArea :class="'h-80 mx-0 w-full rounded-md'">
      <div :class="'flex flex-col'">
        <ODTeacherPageRecentActionItem
          v-for="activityPred of GenerateAcrivity"
          :activityPred="{
            name: activityPred.name,
            data: activityPred.data,
          }"
        ></ODTeacherPageRecentActionItem>
      </div>
    </ScrollArea>
  </Item>
</template>
