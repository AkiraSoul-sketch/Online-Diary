<script setup lang="ts">
import { provide } from "vue";
import StatisticsCard from "../Common/Components/StatisticsCard.vue";
import { useDisciplinesViewManager } from "./disciplines.view.manager";
import DisciplinesFiltersDrawer from "./DisciplinesFiltersDrawer.vue";
import DisciplinesFiltersForm from "./DisciplinesFiltersForm.vue";
import DisciplinesListView from "./DisciplinesListView.vue";
import DisciplinesTableView from "./DisciplinesTableView.vue";
import DisciplinesView from "./DisciplinesView.vue";
import type { DisciplinesViewProps } from "./disciplines.view.props";
import DisciplinesCreateDrawer from "./DisciplinesCreateDrawer.vue";

const viewManager = useDisciplinesViewManager();

const props: DisciplinesViewProps = {
  toggleFiltersDrawer: viewManager.toggleFiltersDrawer,
  toggleCreateDrawer: viewManager.toggleCreateDrawer,
  isFiltersDrawerOpen: (): boolean => viewManager.isFiltersDrawerOpen.value,
  isCreateDrawerOpen: (): boolean => viewManager.isCreateDrawerOpen.value,
  canRenderTable: (): boolean => viewManager.canRenderTable.value,
};

function resolveDesktopMovile(): string {
  return viewManager.canRenderTable.value
    ? "disciplines-page-layout-desktop"
    : "disciplines-page-layout-mobile";
}

provide("disciplinesViewManager", props);

</script>

<template>
  <section :class="['disciplines-page-container', resolveDesktopMovile()]">
    <div :class="'statistics-grid-track statistics-container'">
      <StatisticsCard :class="'stats-total'" :title="'Количество дисциплин'" :value="'10'" />
      <StatisticsCard :title="'Преподаются'" :value="'5'" />
      <StatisticsCard :title="'Не преподаются'" :value="'5'" />
    </div>
    <div v-if="viewManager.canRenderTable.value" :class="'filters-column-track filters-container'">
      <DisciplinesFiltersForm :class="'filter-item'" />
    </div>
    <div :class="'items-column-track'">
      <DisciplinesView :class="'items-container'">
        <template v-if="viewManager.canRenderTable.value">
          <DisciplinesTableView />
        </template>
        <template v-else>
          <DisciplinesListView />
        </template>
      </DisciplinesView>
    </div>
  </section>
  <DisciplinesFiltersDrawer />
  <DisciplinesCreateDrawer />
</template>

<style lang="css" scoped>
.disciplines-page-container {
  height: 100%;
  min-height: 0;
  min-width: 0;

  padding: 1em;
  gap: 1em;
  display: grid;

  grid-template-rows: auto 1fr;
}

.disciplines-page-layout-desktop {
  grid-template-areas:
    "one one one"
    "two three three";
  grid-template-columns: 20% 1fr 1fr;
}

.disciplines-page-layout-mobile {
  grid-template-areas:
    "one one one"
    "three three three";
  grid-template-columns: 1fr 1fr 1fr;
}

.statistics-grid-track {
  grid-area: one;
}

.filters-column-track {
  grid-area: two;
  min-width: 0;
}

.items-column-track {
  grid-area: three;
  min-height: 0;
  min-width: 0;
  display: grid;
  grid-template-columns: 1fr;
}

.statistics-container {
  display: flex;
  flex-wrap: wrap;
  gap: 1em;
}

.filters-container {
  display: flex;
  flex-direction: column;
  gap: 1em;
}

@media (width < 680px) {
  .statistics-container {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 1em;
  }

  .stats-total {
    grid-column: 1 / -1;
  }
}

.items-container {
  min-height: 0;
  min-width: 0;
}

.statistics-container>StatisticsCard {
  flex: 1;
}
</style>
