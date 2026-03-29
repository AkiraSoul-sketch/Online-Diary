<script setup lang="ts">
import ODTeacherPageJournalsContainer from "./Components/OD-TeacherPageJournalsContainer.vue";
import ODTeacherPageRecentActionsContainer from "./Components/OD-TeacherPageRecentActionsContainer.vue";
</script>

<template>
  <div :class="'h-full flex flex-col md:flex-row'">
    <ODTeacherPageJournalsContainer />
    <ODTeacherPageRecentActionsContainer />
  </div>
</template>
