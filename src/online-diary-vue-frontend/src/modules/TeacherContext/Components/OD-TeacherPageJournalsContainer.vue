<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import { ref, watch } from "vue";
import type { Discipline } from "../Models/Discipline";
import { SearchIcon } from "lucide-vue-next";
import ODTeacherDisciplineCard from "./OD-TeacherDisciplineCard.vue";
import { useElementSizeObservability } from "@/modules/Common/Composables/useElementSizeObservability";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import ScrollableContent from "@/modules/Common/Components/ScrollableContent.vue";

const cards: Discipline[] = generate(51);
const container = ref<HTMLElement | null>(null);
const title = ref<HTMLElement | null>(null);
const input = ref<HTMLElement | null>(null);
const containerSize = useElementSizeObservability(container);
const titleSize = useElementSizeObservability(title);
const inputSize = useElementSizeObservability(input);
const scrollAreaLimit = ref(0);

watch(
  () => [
    containerSize.value.height,
    titleSize.value.height,
    inputSize.value.height,
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

function generate(count: number): Discipline[] {
  const DesceplinaName = "Дисциплина 1";

  const Desceplina = [];
  for (let A = 0; A < count; A++) {
    const desceplina: Discipline = {
      name: DesceplinaName,
      groupname: "ПСК-3-25",
      gradescount: 26,
    };
    Desceplina.push(desceplina);
  }
  return Desceplina;
}
</script>

<template>
  <div :class="'flex-1 h-full flex flex-col p-1'" ref="container">
    <Card :class="'flex-1 min-h-0 flex-col gap-1 shadow-(--shadow-basic) p-2'">
      <div ref="title">
        <CardTitle :class="'text-responsive'">Журналы</CardTitle>
      </div>
      <div ref="input">
        <InputWithIcon :icon="SearchIcon" :placeHolder="'Поиск...'" />
      </div>
      <CardContent>
        <ScrollableContent :heightLimit="scrollAreaLimit">
          <div :class="'flex flex-wrap gap-2'">
            <ODTeacherDisciplineCard
              :class="'shrink min-w-35 flex-1 sm:min-w-70 md:min-w-max'"
              v-for="(discipline, index) in cards"
              :key="index"
              v-bind="discipline"
            />
          </div>
        </ScrollableContent>
      </CardContent>
    </Card>
  </div>
</template>
