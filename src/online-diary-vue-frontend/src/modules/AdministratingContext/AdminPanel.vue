<script setup lang="ts">
import AdminPanelMenu from "./component/AdminPanelMenu/AdminPanelMenu.vue";
import AdminActivityChart from "./component/AdminPanelMenu/AdminActivityChart.vue";
import AdminActivityList from "./component/AdminPanelMenu/AdminActivityList.vue";
</script>

<template>
  <section :class="'w-full h-full min-h-0 flex flex-col flex-1'">
    <div :class="'self-center '">
      <AdminPanelMenu
        :items="[
          {
            menuName: 'Система',
            menuItems: ['Аудит'],
            type: 'left',
          },
          {
            menuName: 'Пользователи',
            type: 'item',
          },
          {
            menuName: 'Учебные объекты',
            type: 'right',
          },
        ]"
      />
    </div>
    <AdminActivityChart :class="'flex-1 min-h-0'" />
    <AdminActivityList :class="'flex flex-1 min-h-0'" />
  </section>
</template>
