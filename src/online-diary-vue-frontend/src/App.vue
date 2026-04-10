<script setup lang="ts">
import ODHeader from "./modules/Common/HeaderContext/OD-Header.vue";
import ODFooter from "./modules/Common/FooterContext/OD-Footer.vue";
import ODSideBar from "./modules/Common/SidebarContext/OD-SideBar.vue";
import { useViewPortSizeAdjuster } from "./modules/Common/Composables/useViewPortSizeAdjuster";
import { useAuthenticationStatusStore } from "./modules/Common/Authentication/authentication.status.store";
import VerticalScrollableContent from "./modules/Common/Components/VerticalScrollableContent.vue";
import { useCommonStore } from "./modules/Common/Stores/common.store";
import { useColorMode } from "@vueuse/core";

// для отслеживания размеры вьюпорта, используется для того, чтобы
// писать в common store размеры хедера и футера для вычисления размера вьюпорта.
const viewPortSize = useViewPortSizeAdjuster();

// хранилище размеров. используется чтобы задать размеры вьюпорта.
const commonStore = useCommonStore();

// адаптивная цветовая схема.
useColorMode();

// статус авторизации пользователя. 
useAuthenticationStatusStore();

</script>

<template>
  <section :class="'bg-main w-full h-screen'" :ref="viewPortSize.globalContainer">
    <ODSideBar />
    <div :class="'grid grid-rows-[auto_1fr_auto]'">
      <ODHeader />
      <main :class="'h-full'">
        <VerticalScrollableContent v-if="commonStore.viewPortHeight > 0" :height-limit="commonStore.viewPortHeight">
          <RouterView />
        </VerticalScrollableContent>
      </main>
      <ODFooter />
    </div>
  </section>
</template>
