<script setup lang="ts">
import { ref, watch, type Ref } from "vue";
import StatisticsCard from "../Common/Components/StatisticsCard.vue";
import { useElementSizeObservabilityV2 } from "../Common/Composables/useElementSizeObservabilityV2";
import DisciplinesSection from "./components/DisciplineList/DisciplinesSection.vue";
import DisciplinesListFilters from "./DisciplinesListFilters.vue";
import { useCommonStore } from "../Common/Stores/common.store";
import { useMediaScreenTypeTracker } from "../Common/Composables/useMediaScreenTypeTracker";
import DisciplineForm from "./components/DisciplineForm.vue";
import DisciplineForm_mobile from "./components/DisciplineForm_mobile.vue";

const container = useElementSizeObservabilityV2();
const statistics = useElementSizeObservabilityV2();
const filter = useElementSizeObservabilityV2();
const common = useCommonStore();
const dataContainerHeight: Ref<number> = ref(0);
const { isXS, isSM, isMD, isLG } = useMediaScreenTypeTracker();

// наблюдатель за изменением размеров контейнера, статистики и фильтра
// для динамического расчета доступной высоты для данных дисциплин
watch(
  () => [container.height.value, statistics.height.value, filter.height.value],
  ([$container, $statistics, $filter]) => {
    let height: number = $container - $statistics - common.headerHeight;
    if (isXS()) {
      height -= $filter + 58;
      dataContainerHeight.value = height;
      return;
    }
    if (isSM()) {
      height -= $filter + 58;
      dataContainerHeight.value = height;
      return;
    }
    if (isMD()) {
      height -= $filter + 50;
      dataContainerHeight.value = height;
      return;
    }
    if (isLG()) {
      height -= 38;
      dataContainerHeight.value = height;
      return;
    }
    height -= 24;
    dataContainerHeight.value = height;
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <section
    :ref="container.element"
    :class="'flex-column-layout full-size my-6 px-6 gap-4'"
  >
    <section :ref="statistics.element" class="flex flex-wrap gap-4">
      <StatisticsCard :title="'Всего'" :value="12" />
      <StatisticsCard :title="'Преподаются'" :value="8" />
      <StatisticsCard :title="'Не преподаются'" :value="8" />
    </section>
    <section :class="'flex flex-col gap-2 lg:flex-row'">
      <div :ref="filter.element" :class="'shrink-0 flex flex-col gap-2'">
        <DisciplinesListFilters />
        <DisciplineForm />
      </div>
      <DisciplinesSection
        v-if="dataContainerHeight > 0"
        :data-height-limit="dataContainerHeight"
      />
    </section>
  </section>
  <DisciplineForm_mobile :is-open="false" />
</template>
