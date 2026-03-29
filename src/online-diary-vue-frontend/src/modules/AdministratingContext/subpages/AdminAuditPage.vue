<script setup lang="ts">
import AdminActivityChart from "../components/AdminActivityChart.vue";
import AdminActivityList from "../components/AdminActivityList.vue";
</script>

<template>
  <section
    :class="'w-full h-full min-h-0 flex flex-col flex-1 md:flex-col lg:flex-row'"
  >
    <AdminActivityChart :class="'flex flex-1 min-h-0'" />
    <AdminActivityList :class="'flex flex-1 min-h-0'" />
  </section>
</template>
