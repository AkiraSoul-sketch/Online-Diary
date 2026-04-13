<script setup lang="ts">
import DisciplinesListViewCard from "./DisciplinesListViewCard.vue";
import { useDisciplinesStore } from "./discipline.store";

const store = useDisciplinesStore();
</script>

<template>
  <div class="list-scroll-container">
    <div class="disciplines-list">
      <DisciplinesListViewCard v-for="discipline in store.disciplines" :key="discipline.id" :discipline="discipline" />
    </div>
  </div>
</template>

<style lang="css" scoped>
.list-scroll-container {
  min-height: 0;
  min-width: 0;
  height: 100%;
  overflow: auto;
}

.disciplines-list {
  min-height: 0;
  min-width: 0;
  display: flex;
  flex-wrap: wrap;
  align-content: flex-start;
  gap: 1rem;
  padding: 0 1rem 1rem;
}

@media (width < 768px) {
  .disciplines-list {
    padding: 0 0.75rem 0.75rem;
    gap: 0.75rem;
  }
}
</style>
