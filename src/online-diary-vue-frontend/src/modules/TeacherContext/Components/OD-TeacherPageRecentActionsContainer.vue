<script setup lang="ts">
import ODTeacherPageRecentActionItem from "./OD-TeacherPageRecentActionItem.vue";
import type { Activity } from "../Models/Activity";
import ODTeacherPageJournalTableHeader from "./OD-TeacherPageJournalTableHeader.vue";
import DefaultSearch from "@/modules/Common/Components/DefaultSearch.vue";
import { Card } from "@/components/ui/card";
import CardContent from "@/components/ui/card/CardContent.vue";
import { ScrollArea } from "@/components/ui/scroll-area";
import { ref, watch } from "vue";
import { useElementSizeObservability } from "@/modules/Common/Composables/useElementSizeObservability";
import { Components } from "@/modules/Common/ComponentsLogic/Components";
import ODTeacherDisciplineCardMD from "./adaptive/OD-TeacherDisciplineCard-MD.vue";
import ODTeacherActivityCardMD from "./adaptive/OD-TeacherActivityCard-MD.vue";

const mediaTracker = Components.CreateMediaQueryTracker();
const scrollAreaLimit = ref(0);
const headerRef = ref<HTMLElement | null>(null);
const props = defineProps<{
  containerHeight?: number;
}>();
const headerSize = useElementSizeObservability(headerRef);

watch(
  () => props.containerHeight,
  (newHeight) => {
    const headerHeightValue = headerSize.value.height;
    const containerHeightValue = newHeight ?? 0;
    const newLimit = containerHeightValue - headerHeightValue;
    scrollAreaLimit.value = newLimit;
  },
);

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
</script>

<template>
  <!-- MD screen size -->
  <Card
    v-if="mediaTracker.isMd"
    :class="'p-0 gap-0 shadow-(--shadow-basic) h-full flex-inner'"
  >
    <CardContent :class="'my-5.5 flex flex-col flex-inner'">
      <div ref="headerRef" :class="'self-start w-full'">
        <ODTeacherPageJournalTableHeader
          :header-value="'Недавняя активность:'"
        />
        <DefaultSearch :class="'rounded-sm shadow-(--shadow-basic)'" />
      </div>
      <ScrollArea
        :class="'my-5'"
        :style="{
          height: `${scrollAreaLimit}px`,
        }"
      >
        <ODTeacherActivityCardMD
          v-for="activity of generateAcrivity(50)"
          v-bind="activity"
        />
      </ScrollArea>
    </CardContent>
  </Card>
  <!-- LG XL screen sizes -->
  <Card v-else :class="'p-0 gap-0 shadow-(--shadow-basic) h-full flex-inner'">
    <CardContent :class="'my-5.5 flex flex-col flex-inner'">
      <div ref="headerRef" :class="'self-start w-full'">
        <ODTeacherPageJournalTableHeader
          :header-value="'Недавняя активность:'"
        />
        <DefaultSearch :class="'rounded-sm shadow-(--shadow-basic)'" />
      </div>
      <ScrollArea
        :class="'mx-0 rounded-sm overflow-auto shadow-(--shadow-basic) my-3'"
        :style="{
          height: `${scrollAreaLimit}px`,
        }"
      >
        <ODTeacherPageRecentActionItem
          v-for="activityPred of generateAcrivity(50)"
          :activityPred="{
            name: activityPred.name,
            data: activityPred.data,
          }"
        ></ODTeacherPageRecentActionItem>
      </ScrollArea>
    </CardContent>
  </Card>
</template>
