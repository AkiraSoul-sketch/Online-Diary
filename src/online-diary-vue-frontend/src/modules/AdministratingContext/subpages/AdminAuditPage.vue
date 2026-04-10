<script setup lang="ts">
import { useAdminSizeStore } from "../adminSize.store";
import AdminActivityChart from "../components/AdminActivityChart.vue";
import AdminActivityList from "../components/AdminActivityList.vue";

const adminSizeStore = useAdminSizeStore();


// TODO: fix
</script>

<template>
  <section :class="'p-2 flex flex-col h-full w-full lg:flex-row lg:gap-2'">
    <div :class="'w-full'">
      <AdminActivityChart />
    </div>
    <div :class="'w-full'">
      <AdminActivityList />
    </div>
    <!-- <div :class="'flex-1 min-w-0 min-h-100'">
      <AdminActivityChart />
    </div>
    <div :class="'flex-1 min-w-0 min-h-0'">
      <AdminActivityList />
    </div> -->
  </section>
</template>
