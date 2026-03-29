<script setup lang="ts">
import AdminPanelMenu from "./component/AdminPanelMenu/AdminPanelMenu.vue";

// TODO: 2. Переработать страницу Пользователи
// TODO: 3. Переработать страницу учебные планы.
// TODO: 4. Добавить роутинг для кнопок категорий меню.
// TODO: 5. Добавить возможность ховера с появлением подменю для категорий меню, у которых есть элементы меню.
</script>

<template>
  <section :class="'w-full h-full min-h-0 flex flex-col flex-1'">
    <div :class="'self-center '">
      <AdminPanelMenu />
    </div>
    <section :class="'flex flex-1 min-h-0'">
      <RouterView />
    </section>
  </section>
</template>
