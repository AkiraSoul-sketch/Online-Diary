<script setup lang="ts">
import { ref } from "vue";
import { Components } from "../Common/ComponentsLogic/Components";
import ODTeacherPageJournalsContainer from "./Components/OD-TeacherPageJournalsContainer.vue";
import ODTeacherPageRecentActionsContainer from "./Components/OD-TeacherPageRecentActionsContainer.vue";
import { useElementSizeObservability } from "../Common/Composables/useElementSizeObservability";

const mediaTracker = Components.CreateMediaQueryTracker();
const disciplinesList = ref<HTMLElement | null>(null);
const activityList = ref<HTMLElement | null>(null);
const disciplinesListSize = useElementSizeObservability(disciplinesList);
const activityListSize = useElementSizeObservability(activityList);
</script>

<template>
  <div v-if="mediaTracker.isXl" :class="'flex full-size flex-inner'">
    <section :class="'grid grid-cols-2 p-2 gap-2 w-full'">
      <div ref="disciplinesList" :class="'flex flex-inner'">
        <ODTeacherPageJournalsContainer
          :container-height="disciplinesListSize.height"
        />
      </div>
      <div ref="activityList" :class="'flex full-size flex-inner'">
        <ODTeacherPageRecentActionsContainer
          :container-height="activityListSize.height"
        />
      </div>
    </section>
  </div>

  <div v-if="mediaTracker.isLg" :class="'flex full-size flex-inner'">
    <section :class="'p-2 gap-2 flex flex-row full-size flex-inner'">
      <div ref="disciplinesList">
        <ODTeacherPageJournalsContainer
          :container-height="activityListSize.height"
        />
      </div>
      <div ref="activityList">
        <ODTeacherPageRecentActionsContainer
          :container-height="activityListSize.height"
        />
      </div>
    </section>
  </div>

  <!-- TODO: Finish for MD SM and XS -->

  <div
    v-else-if="mediaTracker.isMd"
    :class="'flex full-size flex-row flex-1 min-w-0 p-2 gap-2'"
  >
    <div ref="disciplinesList" :class="'flex-1 min-w-0'">
      <ODTeacherPageJournalsContainer
        :container-height="activityListSize.height"
      />
    </div>

    <div ref="activityList" :class="'flex-1 min-w-0'">
      <ODTeacherPageRecentActionsContainer
        :container-height="activityListSize.height"
      />
    </div>
  </div>

  <div v-else-if="mediaTracker.isSm">
    <div>Sm content</div>
  </div>

  <div v-else-if="mediaTracker.isXs">
    <div>XS content</div>
  </div>
</template>
