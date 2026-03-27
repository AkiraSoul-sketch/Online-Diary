<script lang="ts">
import { Card } from "@/components/ui/card";
import ODTeacherPageJournalsContainer from "./Components/OD-TeacherPageJournalsContainer.vue";
import ODTeacherPageRecentActionsContainer from "./Components/OD-TeacherPageRecentActionsContainer.vue";

export default {
  components: {
    Card,
    ODTeacherPageJournalsContainer,
    ODTeacherPageRecentActionsContainer,
  },
};
</script>

<template>
  <section :class="'p-2 gap h-full flex flex-col w-full'">
    <div :class="'p-2 flex-1'">
      <ODTeacherPageJournalsContainer />
    </div>
    <div :class="' p-2 flex-1'">
      <ODTeacherPageRecentActionsContainer />
    </div>
  </section>
</template>
