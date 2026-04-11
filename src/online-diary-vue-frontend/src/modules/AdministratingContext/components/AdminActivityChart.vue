<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import { type ChartConfig } from "@/components/ui/chart";
import ChartContainer from "@/components/ui/chart/ChartContainer.vue";
import { useCommonStore } from "@/modules/Common/Stores/common.store";
import {
  VisTooltip,
  VisXYContainer,
  VisStackedBar,
  VisAxis,
  VisCrosshair,
} from "@unovis/vue";
import CardHeader from "@/components/ui/card/CardHeader.vue";

const chartData = [
  { weekDay: 1, activity: 10 },
  { weekDay: 2, activity: 12 },
  { weekDay: 3, activity: 14 },
  { weekDay: 4, activity: 16 },
  { weekDay: 5, activity: 14 },
  { weekDay: 6, activity: 12 },
  { weekDay: 7, activity: 12 },
];

type Data = (typeof chartData)[number];

const days = ["Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"];

function resolveWeekDayText(i: number): string {
  return days[i - 1];
}

// используется, чтобы обновлять график при ресайзе страницы.
const commonStore = useCommonStore();

const chartConfig = {
  activity: {
    label: "Активность",
    color: "#CCCCCC 50%",
  },
} satisfies ChartConfig;

</script>

<template>
  <Card :class="'card-primary borderless shadow-none rounded-sm h-full w-full min-h-0 min-w-0'">
    <CardHeader>
      <CardTitle :class="'font-normal text-responsive-primary'">Активность</CardTitle>
    </CardHeader>
    <CardContent :class="'h-full min-h-0 w-full min-w-0'">
      <ChartContainer :config="chartConfig" :width="100">
        <VisXYContainer :key="commonStore.viewPortWidth" :data="chartData">
          <VisStackedBar :x="(d: Data) => d.weekDay" :y="(d: Data) => d.activity" :color="chartConfig.activity.color" />

          <VisAxis type="x" label="День недели" :tick-format="(i: number) => {
            if (i < 1 || i > 7) return;
            return resolveWeekDayText(i);
          }
            " :grid-line="true" :tickValues="chartData.map((d) => d.weekDay)" />
          <VisAxis type="y" label="Активность" />
          <VisTooltip />
          <VisCrosshair :x="(d: Data) => d.weekDay" :y="(d: Data) => d.activity" :color="'#a1fb95'"
            :strokeColor="'#000000'" :strokeWidth="'5px'" :template="(d: Data) =>
              [d.activity, resolveWeekDayText(d.weekDay)].join(', ')
              " />
        </VisXYContainer>
      </ChartContainer>
    </CardContent>
  </Card>
</template>

<style scoped lang="css">
::v-deep svg path.css-ix1mbc-bar {
  stroke: #000 !important;
  stroke-width: 1px !important;
}
</style>
