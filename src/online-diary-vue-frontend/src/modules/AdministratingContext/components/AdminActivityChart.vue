<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import { type ChartConfig } from "@/components/ui/chart";
import ChartContainer from "@/components/ui/chart/ChartContainer.vue";
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

const chartConfig = {
  activity: {
    label: "Активность",
    color: "#CCCCCC 50%",
  },
} satisfies ChartConfig;

</script>

<template>
  <Card :class="'card-primary borderless shadow-none chart-card'">
    <CardHeader>
      <CardTitle :class="'text-responsive-primary'">Активность</CardTitle>
    </CardHeader>
    <CardContent :class="'chart-card-content'">
      <ChartContainer :config="chartConfig" :class="'chart-container'">
        <VisXYContainer :data="chartData" :class="'vis-chart'">
          <VisStackedBar :x="(d: Data) => d.weekDay" :y="(d: Data) => d.activity" :color="chartConfig.activity.color"
            :class="'stacked-bar'" />

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
.chart-card {
  min-height: 0;
  min-width: 0;
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: auto 1fr;
}

.chart-card-content {
  min-height: 0;
  min-width: 0;
  display: flex;
  flex-direction: column;
}

.chart-container {
  min-height: 0;
  min-width: 0;
  display: flex;
  flex-direction: column;
}

.vis-chart {
  min-height: 0;
  min-width: 0;
  flex: 1;
  display: flex;
}

.stacked-bar {
  min-height: 0;
  min-width: 0;
  flex: 1;
}

::v-deep svg path.css-ix1mbc-bar {
  stroke: #000 !important;
  stroke-width: 1px !important;
}
</style>
