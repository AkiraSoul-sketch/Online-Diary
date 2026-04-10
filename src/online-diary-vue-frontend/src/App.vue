<script setup lang="ts">
import ODHeader from "./modules/Common/HeaderContext/OD-Header.vue";
import ODFooter from "./modules/Common/FooterContext/OD-Footer.vue";
import ODSideBar from "./modules/Common/SidebarContext/OD-SideBar.vue";
import { useGlobalContainerWidthTracker } from "./modules/Common/Composables/useGlobalContainerWidthTracker";
import { useViewPortSize } from "./modules/Common/Composables/useViewPortSize";
import { useAuthenticationStatusStore } from "./modules/Common/Authentication/authentication.status.store";
import VerticalScrollableContent from "./modules/Common/Components/VerticalScrollableContent.vue";

// для отслеживания размеры вьюпорта, используется для того, чтобы
// контент внутри RouterView рендерился с вертикальным скроллбаром, а не все окно скроллился.
const viewPortSize = useViewPortSize();

// для отслеживания глобального контейнера страницы.
// Используется, чтобы обновлять размеры графика в admin activity page.
const widthTracker = useGlobalContainerWidthTracker();

// адаптивная цветовая схема.
// useColorMode();

// статус авторизации пользователя. 
useAuthenticationStatusStore();

</script>

<template>
  <section :class="'bg-main w-full h-screen'" :ref="widthTracker.container">
    <ODSideBar />
    <div :class="'grid grid-rows-[auto_1fr_auto]'">
      <ODHeader />
      <main :class="'h-full'" :ref="viewPortSize.viewport">
        <VerticalScrollableContent :height-limit="viewPortSize.height.value">
          <RouterView />
        </VerticalScrollableContent>
      </main>
      <ODFooter />
    </div>
  </section>
</template>
