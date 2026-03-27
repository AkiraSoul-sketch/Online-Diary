<script setup lang="ts">
import { ref } from "vue";
import { Components } from "../Common/ComponentsLogic/Components";
import ODTeacherPageJournalsContainer from "./Components/OD-TeacherPageJournalsContainer.vue";
import ODTeacherPageRecentActionsContainer from "./Components/OD-TeacherPageRecentActionsContainer.vue";
import {
  getObservedElementHeight,
  useElementSizeObservability,
} from "../Common/Composables/useElementSizeObservability";

const mediaTracker = Components.CreateMediaQueryTracker();
const disciplinesList = ref<HTMLElement | null>(null);
const activityList = ref<HTMLElement | null>(null);
const disciplinesListSize = useElementSizeObservability(disciplinesList);
const activityListSize = useElementSizeObservability(activityList);
</script>

<template>
  <div v-if="mediaTracker.isXl" :class="'flex flex-1 h-full w-full min-h-0'">
    <section :class="'grid grid-cols-2 p-2 gap-2 h-full w-full flex-1 min-h-0'">
      <div ref="disciplinesList">
        <ODTeacherPageJournalsContainer
          :class="'flex flex-col h-full min-h-0'"
          :container-height="getObservedElementHeight(disciplinesListSize) - 72"
        />
      </div>
      <div ref="activityList">
        <ODTeacherPageRecentActionsContainer
          :class="'flex flex-col h-full min-h-0'"
          :container-height="getObservedElementHeight(activityListSize)"
        />
      </div>
    </section>
  </div>

  <div v-if="mediaTracker.isLg" :class="'flex flex-1 h-full w-full min-h-0'">
    <section :class="'p-2 gap-2 h-full flex flex-row w-full flex-1 min-h-0'">
      <div ref="disciplinesList">
        <ODTeacherPageJournalsContainer
          :class="'flex flex-1 min-h-0'"
          :container-height="getObservedElementHeight(activityListSize)"
        />
      </div>
      <div ref="activityList">
        <ODTeacherPageRecentActionsContainer
          :class="'flex flex-1 min-h-0'"
          :container-height="getObservedElementHeight(activityListSize)"
        />
      </div>
    </section>
  </div>

  <!-- TODO: Finish for MD SM and XS -->

  <div
    v-else-if="mediaTracker.isMd"
    :class="'flex flex-1 h-full w-full min-h-0'"
  >
    <div>MD content</div>
  </div>

  <div v-else-if="mediaTracker.isSm">
    <div>Sm content</div>
  </div>

  <div v-else-if="mediaTracker.isXs">
    <div>XS content</div>
  </div>
</template>
