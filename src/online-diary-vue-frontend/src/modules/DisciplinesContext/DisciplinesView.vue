<script setup lang="ts">
import { ListFilterPlusIcon, PlusIcon } from "lucide-vue-next";
import { Button } from "@/components/ui/button";
import { inject } from "vue";
import type { DisciplinesViewProps } from "./disciplines.view.props";

const viewManager = inject<DisciplinesViewProps>("disciplinesViewManager");
</script>

<template>
  <section :class="'items-container card-primary rounded-md border'">
    <div :class="'p-4 flex-column-layout rounded-t-md'">
      <div :class="'text-responsive-primary font-semibold text-white'">
        Список дисциплин
      </div>
      <div :class="'flex-row-layout gap-2'">
        <div class="flex items-center gap-4 ml-auto">
          <Button v-on:click="viewManager?.toggleFiltersDrawer" v-if="!viewManager?.canRenderTable()"
            :variant="'quaternary'" class="flex items-center gap-2">
            <ListFilterPlusIcon :size="16" />
            <span class="text-responsive-secondary">Фильтры</span>
          </Button>
          <Button :variant="'sixth'" class="flex items-center gap-2" v-on:click="viewManager?.toggleCreateDrawer">
            <PlusIcon :size="16" />
            <span class="text-responsive-secondary">Создать</span>
          </Button>
        </div>
      </div>
    </div>
    <div :class="'items-scroll'">
      <slot />
    </div>
  </section>
</template>

<style lang="css" scoped>
.items-container {
  min-height: 0;
  min-width: 0;
  display: flex;
  flex-direction: column;
}

.items-scroll {
  min-height: 0;
  flex: 1;
  overflow: auto;
}
</style>
