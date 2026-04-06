<script setup lang="ts">
import AdminActivityChart from "../components/AdminActivityChart.vue";
import AdminActivityList from "../components/AdminActivityList.vue";
</script>

<template>
  <section :class="'full-size flex-constrained-column flex flex-col lg:flex-row'">
    <AdminActivityChart :class="'flex flex-constrained-column'" />
    <AdminActivityList :class="'flex flex-constrained-column'" />
  </section>
</template>
