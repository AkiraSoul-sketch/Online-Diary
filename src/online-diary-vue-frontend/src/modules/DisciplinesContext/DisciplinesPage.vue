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
import DisciplinesListFilters_mobile from "./DisciplinesListFilters_mobile.vue";
import { useMobileDisciplines } from "./discipline.mobile";

const container = useElementSizeObservabilityV2();
const statistics = useElementSizeObservabilityV2();
const filter = useElementSizeObservabilityV2();
const common = useCommonStore();
const { isOpen, closeMobileFilters, toggleMobileFilters } = useMobileDisciplines();
const dataContainerHeight: Ref<number> = ref(0);
const { isXS, isSM, isMD, isLG, isXL, isXXL, } = useMediaScreenTypeTracker();

// наблюдатель за изменением размеров контейнера, статистики и фильтра
// для динамического расчета доступной высоты для данных дисциплин
watch(
  () => [container.height.value, statistics.height.value, filter.height.value],
  ([$container, $statistics, $filter]) => {
    let height: number = $container - $statistics;
    if (isXS()) {
      height -= $filter + 58 + common.headerHeight;
      dataContainerHeight.value = height;
      return;
    }
    if (isSM()) {
      height -= $filter + 58 + common.headerHeight;
      dataContainerHeight.value = height;
      return;
    }
    if (isMD()) {
      height -= $filter + 50 + common.headerHeight;
      dataContainerHeight.value = height;
      return;
    }
    if (isLG()) {
      height -= 38;
      dataContainerHeight.value = height;
      return;
    }
    dataContainerHeight.value = height;
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <section :ref="container.element" :class="'disciplines-page-container'">
    <div :ref="statistics.element" :class="'statistics-container one'">
      <StatisticsCard :title="'Всего'" :value="12" />
      <StatisticsCard :title="'Преподаются'" :value="8" />
      <StatisticsCard :title="'Не преподаются'" :value="8" />
    </div>
    <div :class="'aside-container two'">
      <DisciplinesListFilters :class="'filters-container'" />
      <DisciplineForm :class="'selected-container'" />
    </div>
    <div :class="'three scroller'">
      <DisciplinesSection :class="'items-container'" @open-mobile-filters="toggleMobileFilters"
        :data-height-limit="dataContainerHeight" />
    </div>
  </section>
</template>

<style lang="css" scoped>
.disciplines-page-container {
  height: 100%;
  min-height: 0;
  min-width: 0;
  box-sizing: border-box;

  padding: 1em;
  gap: 1em;
  display: grid;

  grid-template-areas:
    "one one one"
    "two three three"
    "two three three";

  grid-template-columns: repeat(3, minmax(0, 1fr));
  grid-template-rows: 148px minmax(0, 1fr) minmax(0, 1fr);
}

.one {
  grid-area: one;
}

.two {
  grid-area: two;
}

.three {
  grid-area: three;
  min-width: 0;
  min-height: 0;

  display: flex;
  flex-direction: column;
  overflow: auto;
  box-sizing: border-box;
  padding-bottom: 1em;
}

.three>* {
  flex: 1 1 auto;
  min-width: 0;
  min-height: 0;
}

.aside-container {
  display: flex;
  flex-direction: column;
  gap: 1em;
  min-width: 0;
  min-height: 0;
}

.selected-container {
  flex: 1 1 auto;
  min-width: 0;
  min-height: 0;
}

.statistics-container {
  display: flex;
  flex-wrap: wrap;
  gap: 1em;
  height: 2.5em;
}
</style>
