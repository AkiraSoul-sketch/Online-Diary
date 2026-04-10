<script setup lang="ts">
import AdminPanelMenu from "./components/AdminPanelMenu/AdminPanelMenu.vue";
import { useAdminStore } from "./admin.store";
import { useAdminSizeHeaderHeightTracker } from "./composables/adminSizeheaderHeightTracker";
import { useAdminSizeStore } from "./adminSize.store";

const adminStore = useAdminStore();
const adminSizeStore = useAdminSizeStore();
const headerTracker = useAdminSizeHeaderHeightTracker();

// TODO: 2. Переработать страницу Пользователи
// TODO: 3. Переработать страницу учебные планы.
// TODO: 4. Добавить роутинг для кнопок категорий меню.
// TODO: 5. Добавить возможность ховера с появлением подменю для категорий меню, у которых есть элементы меню.
</script>

<template>
  <section :class="'flex-column-layout'">
    <div :class="'flex-column-layout w-full self-center'" :ref="headerTracker.adminHeader">
      <AdminPanelMenu :class="'self-center'" />
      <h3 :class="'text-responsive-primary font-semibold mx-10 self-start py-3'">
        {{ adminStore.menuTitle }}
      </h3>
    </div>
    <div :style="{
      height: `${adminSizeStore.heightLimit}px`
    }">
      <RouterView />
    </div>
  </section>
</template>
