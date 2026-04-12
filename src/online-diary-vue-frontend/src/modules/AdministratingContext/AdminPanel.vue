<script setup lang="ts">
import AdminPanelMenu from "./components/AdminPanelMenu/AdminPanelMenu.vue";

// TODO: 2. Переработать страницу Пользователи
// TODO: 3. Переработать страницу учебные планы.
// TODO: 4. Добавить роутинг для кнопок категорий меню.
// TODO: 5. Добавить возможность ховера с появлением подменю для категорий меню, у которых есть элементы меню.
</script>

<template>
  <section class="admin-panel-container">
    <AdminPanelMenu />
    <RouterView :class="'admin-panel-content-slot'" />
  </section>
</template>

<style scoped lang="css">
.admin-panel-container {
  flex: 1;
  min-height: 0;
  min-width: 0;
  display: grid;
  grid-template-rows: auto 1fr;
}

.admin-panel-content-slot {
  min-height: 0;
  min-width: 0;
  display: flex;
  flex: 1;
  flex-direction: column;
}
</style>