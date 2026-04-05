<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import { type ChartConfig } from "@/components/ui/chart";
import ChartContainer from "@/components/ui/chart/ChartContainer.vue";
import { useGlobalContainerStore } from "@/modules/Common/Stores/globalContainer.store";
import {
  VisTooltip,
  VisXYContainer,
  VisStackedBar,
  VisAxis,
  VisCrosshair,
} from "@unovis/vue";

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
const globalContainerStore = useGlobalContainerStore();

const chartConfig = {
  activity: {
    label: "Активность",
    color: "#CCCCCC 50%",
  },
} satisfies ChartConfig;
</script>

<template>
  <div :class="'p-2  min-h-0 min-w-0 shrink'" ref="container">
    <Card
      :class="'card-primary p-0 flex-column-layout flex-constrained-column shrink border-none shadow-(--shadow-basic) rounded-sm'">
      <CardTitle :class="'item-bg-primary font-normal p-2 text-responsive-primary'">Активность</CardTitle>
      <CardContent :class="'card-primary flex-constrained shrink '">
        <ChartContainer :config="chartConfig">
          <VisXYContainer :key="globalContainerStore.width" :data="chartData">
            <VisStackedBar :x="(d: Data) => d.weekDay" :y="(d: Data) => d.activity"
              :color="chartConfig.activity.color" />

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
  </div>
</template>

<style scoped lang="css">
::v-deep svg path.css-ix1mbc-bar {
  stroke: #000 !important;
  stroke-width: 1px !important;
}
</style>
