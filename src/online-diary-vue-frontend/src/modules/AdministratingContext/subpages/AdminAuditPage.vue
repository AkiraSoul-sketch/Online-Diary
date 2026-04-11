<script setup lang="ts">
import { useAdminSizeStore } from "../adminSize.store";
import AdminActivityChart from "../components/AdminActivityChart.vue";
import AdminActivityList from "../components/AdminActivityList.vue";

const adminSizeStore = useAdminSizeStore();


// TODO: fix
</script>

<template>
  <section :class="'p-2'">
    <div :class="'w-full gap-2 flex flex-col xl:grid xl:grid-cols-2'" :style="{
      height: `${adminSizeStore.heightLimit}px`
    }">
      <AdminActivityChart />
      <AdminActivityList />
    </div>
  </section>
</template>
