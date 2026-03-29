<script setup lang="ts">
import type { Activity } from "../Models/Activity";
import { Card, CardContent, CardTitle } from "@/components/ui/card";
import { ScrollArea } from "@/components/ui/scroll-area";
import { ref, watch } from "vue";
import ODTeacherActivityCardMD from "./OD-TeacherActivityCard.vue";
import ScrollBar from "@/components/ui/scroll-area/ScrollBar.vue";
import { SearchIcon } from "lucide-vue-next";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";

const activities: Activity[] = generateAcrivity(51);
const scrollAreaLimit = ref(0);
const containerSize = useElementSizeObservabilityV2();
const titleSize = useElementSizeObservabilityV2();
const inputSize = useElementSizeObservabilityV2();

watch(
  () => [
    containerSize.height.value,
    titleSize.height.value,
    inputSize.height.value,
  ],
  ([$containerHeight, $titleHeight, $inputHeight]) => {
    scrollAreaLimit.value = Math.max(
      0,
      $containerHeight - $titleHeight - $inputHeight - 32,
    );
  },
  {
    immediate: true,
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
  <div :class="'flex flex-col p-1 flex-1 h-full'" :ref="containerSize.element">
    <Card
      :class="'flex-1 min-h-0 flex flex-col gap-1 shadow-(--shadow-basic) p-2'"
    >
      <div :ref="titleSize.element">
        <CardTitle :class="'text-responsive'">Недавние дейсвия</CardTitle>
      </div>
      <div :ref="inputSize.element">
        <InputWithIcon :icon="SearchIcon" :place-holder="'Поиск...'" />
      </div>
      <CardContent>
        <ScrollArea
          :style="{
            height: `${scrollAreaLimit}px`,
          }"
        >
          <div :class="'flex flex-col gap-2 justify-between'">
            <ODTeacherActivityCardMD
              v-for="(activity, index) in activities"
              :key="index"
              v-bind="activity"
            />
          </div>
          <ScrollBar />
        </ScrollArea>
      </CardContent>
    </Card>
  </div>
</template>
