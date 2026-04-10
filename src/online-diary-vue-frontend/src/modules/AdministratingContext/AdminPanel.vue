<script setup lang="ts">
import AdminPanelMenu from "./components/AdminPanelMenu/AdminPanelMenu.vue";
import { useAdminStore } from "./admin.store";

const adminStore = useAdminStore();

// TODO: 2. Переработать страницу Пользователи
// TODO: 3. Переработать страницу учебные планы.
// TODO: 4. Добавить роутинг для кнопок категорий меню.
// TODO: 5. Добавить возможность ховера с появлением подменю для категорий меню, у которых есть элементы меню.
</script>

<template>
  <section :class="'full-size flex-column-layout flex-constrained-column'">
    <div :class="'flex flex-col w-full self-center'">
      <AdminPanelMenu :class="'self-center'" />
      <h3 :class="'text-responsive-primary font-semibold  mx-10 self-start py-3'">
        {{ adminStore.menuTitle }}
      </h3>
    </div>
    <section :class="'flex flex-1 min-h-0'">
      <RouterView />
    </section>
  </section>
</template>
