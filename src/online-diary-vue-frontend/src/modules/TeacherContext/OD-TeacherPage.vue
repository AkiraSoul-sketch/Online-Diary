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
  <section :class="'p-2 h-screen flex flex-col w-full'">
    <Card :class="'py-2 gap-0 flex flex-col w-full'">
      <div :class="'grow p-2'">
        <ODTeacherPageJournalsContainer />
      </div>
      <div :class="'p-2'">
        <ODTeacherPageRecentActionsContainer />
      </div>
    </Card>
  </section>
</template>
