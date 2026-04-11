<script setup lang="ts">
import AdminPanelMenu from "./components/AdminPanelMenu/AdminPanelMenu.vue";

// TODO: 2. Переработать страницу Пользователи
// TODO: 3. Переработать страницу учебные планы.
// TODO: 4. Добавить роутинг для кнопок категорий меню.
// TODO: 5. Добавить возможность ховера с появлением подменю для категорий меню, у которых есть элементы меню.
</script>

<template>
  <section :class="'grid grid-rows-[auto_1fr]'">
    <div>
      <AdminPanelMenu :class="'self-center'" />
    </div>
    <RouterView />
  </section>
</template>
