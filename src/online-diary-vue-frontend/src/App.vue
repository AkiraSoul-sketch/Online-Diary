<script setup lang="ts">
import ODHeader from "./modules/Common/HeaderContext/OD-Header.vue";
import ODFooter from "./modules/Common/FooterContext/OD-Footer.vue";
import ODSideBar from "./modules/Common/SidebarContext/OD-SideBar.vue";
import { useViewPortSizeAdjuster } from "./modules/Common/Composables/useViewPortSizeAdjuster";
import { useAuthenticationStatusStore } from "./modules/Common/Authentication/authentication.status.store";
import { useColorMode } from "@vueuse/core";

// для отслеживания размеры вьюпорта, используется для того, чтобы
// писать в common store размеры хедера и футера для вычисления размера вьюпорта.
const viewPortSize = useViewPortSizeAdjuster();

// адаптивная цветовая схема.
useColorMode();

// статус авторизации пользователя. 
useAuthenticationStatusStore();

</script>

<template>
  <section class="layout" :ref="viewPortSize.globalContainer">

    <ODHeader />
    <RouterView :class="'content-slot'" />
    <ODFooter />

  </section>
  <ODSideBar />
</template>

<style scoped lang="css">
.layout {
  height: 100dvh;
  width: 100dvw;
  display: grid;
  grid-template-rows: auto 1fr auto;
  grid-template-columns: 1fr;
}

.content-slot {
  min-height: 0;
  min-width: 0;
  overflow: auto;
  display: flex;
  flex-direction: column;
}
</style>
