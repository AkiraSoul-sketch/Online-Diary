<script setup lang="ts">
import ODHeader from "./modules/Common/HeaderContext/OD-Header.vue";
import ODFooter from "./modules/Common/FooterContext/OD-Footer.vue";
import ODSideBar from "./modules/Common/SidebarContext/OD-SideBar.vue";
import { useGlobalContainerWidthTracker } from "./modules/Common/Composables/useGlobalContainerWidthTracker";
import { useViewPortReadiness } from "./modules/Common/Composables/useViewportReadiness";
import { useAuthenticationStatusStore } from "./modules/Common/Authentication/authentication.status.store";
import { useMediaScreenTypeTracker } from "./modules/Common/Composables/useMediaScreenTypeTracker";
import { useColorMode } from "@vueuse/core";

// для отслеживания размеры вьюпорта, используется для того, чтобы не рендерить страницу
// до тех пор, пока не будут известны размеры вьюпорта,
// так как от этого зависит отображение некоторых компонентов.
const viewportReadiness = useViewPortReadiness();

// для отслеживания глобального контейнера страницы.
// Используется, чтобы обновлять размеры графика в admin activity page.
const widthTracker = useGlobalContainerWidthTracker();

// адаптивная цветовая схема.
// useColorMode();

// статус авторизации пользователя. 
useAuthenticationStatusStore();

const { isXS, isSM, isMD, isLG, isXL, isXXL } = useMediaScreenTypeTracker();

</script>

<template>
  <section :class="'bg-main w-full h-screen'" :ref="widthTracker.container">
    <ODSideBar />
    <div :class="'grid grid-rows-[auto_1fr_auto] h-full'">
      <ODHeader />
      <main :class="'flex-1 min-h-0'" :ref="viewportReadiness.viewport">
        <RouterView v-if="viewportReadiness.ready" />
        <ODFooter v-if="isLG() || isXL() || isXXL()" />
      </main>
      <ODFooter v-if="isXS() || isSM() || isMD()" />
    </div>
  </section>
</template>
