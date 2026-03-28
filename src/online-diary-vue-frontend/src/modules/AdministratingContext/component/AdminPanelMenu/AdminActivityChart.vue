<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import type {
  ChartConfig,
  ChartTooltip,
  ChartCrosshair,
  ChartContainer,
} from "@/components/ui/chart";
import { VisXYContainer, VisArea, VisLine, VisAxis } from "@unovis/vue";

const chartData = [
  { date: new Date("2024-01-01"), desktop: 186, mobile: 80 },
  { date: new Date("2024-02-01"), desktop: 305, mobile: 200 },
  { date: new Date("2024-03-01"), desktop: 237, mobile: 120 },
  { date: new Date("2024-04-01"), desktop: 73, mobile: 190 },
  { date: new Date("2024-05-01"), desktop: 209, mobile: 130 },
  { date: new Date("2024-06-01"), desktop: 214, mobile: 140 },
];

type Data = (typeof chartData)[number];

const chartConfig = {
  desktop: {
    label: "Desktop",
    color: "#2563eb",
  },
  mobile: {
    label: "Mobile",
    color: "#60a5fa",
  },
} satisfies ChartConfig;
</script>

<template>
  <Card :class="'p-2 my-2 flex-1 min-h-0'">
    <CardTitle>График активностей</CardTitle>
    <CardContent
      :class="'p-2 flex-1 min-h-0 shadow-(--shadow-basic) rounded-sm'"
    >
      <ChartContainer :config="chartConfig">
        <VisXYContainer :data="chartData">
          <VisArea
            :x="(d: Data) => d.date"
            :y="[(d: Data) => d.mobile, (d: Data) => d.desktop]"
            :color="
              (_d: Data, i: number) =>
                [chartConfig.mobile.color, chartConfig.desktop.color][i]
            "
            :opacity="0.4"
          />
          <VisLine
            :x="(d: Data) => d.date"
            :y="[(d: Data) => d.mobile, (d: Data) => d.desktop + d.mobile]"
          />
          <VisAxis
            type="x"
            :x="(d: Data) => d.date"
            :tick-line="false"
            :domain-line="false"
            :grid-line="false"
            :num-ticks="6"
            :tick-format="
              (_: number, index: number) => {
                const date = chartData[index].date as Date;
                return date.toLocaleString('default', { month: 'short' });
              }
            "
          />
          <VisAxis
            :type="'y'"
            :num-ticks="3"
            :tick-line="false"
            :domain-line="false"
          />
        </VisXYContainer>
        <ChartTooltip />
        <ChartCrosshair />
      </ChartContainer>
    </CardContent>
  </Card>
</template>
