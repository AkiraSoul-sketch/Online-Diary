<script setup lang="ts">
import { watch } from "vue";
import ODTeacherPageJournalsContainer from "./Components/OD-TeacherPageJournalsContainer.vue";
import { useCommonStore } from "@/modules/Common/Stores/common.store";
import ODTeacherPageRecentActionsContainer from "./Components/OD-TeacherPageRecentActionsContainer.vue";

const commonStore = useCommonStore();

watch(
  () => commonStore.$state.viewPortHeight,
  (val) => {
    console.log("Teacher page seed viewport height change:", val);
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <div :class="'h-full flex flex-col md:flex-row'">
    <ODTeacherPageJournalsContainer
      v-if="commonStore.$state.viewPortHeight > 0"
    />
    <ODTeacherPageRecentActionsContainer
      :class="'flex-1 min-h-0'"
      v-if="commonStore.$state.viewPortHeight > 0"
    />
  </div>
</template>
