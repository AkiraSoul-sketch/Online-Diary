<script setup lang="ts">

import AdminActivityChart from "../components/AdminActivityChart.vue";
import AdminActivityList from "../components/AdminActivityList.vue";

</script>

<template>
  <section :class="'grid grid-rows-3 p-2 gap-2 h-full min-h-0 lg:grid-cols-2'">
    <AdminActivityChart />
    <div :class="'row-span-2 bg-amber-300/50'">
      <AdminActivityList />
    </div>
  </section>
</template>