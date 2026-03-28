<script setup lang="ts">
import ODHeader from "./modules/Common/HeaderContext/OD-Header.vue";
import { nextTick, onMounted, ref, type Ref } from "vue";
import ODFooter from "./modules/Common/FooterContext/OD-Footer.vue";
import { useCommonStore } from "./common.store";
import ODSideBar from "./modules/Common/SidebarContext/OD-SideBar.vue";

const viewport: Ref<HTMLElement | null> = ref(null);
const store = useCommonStore();

function viewPortSizeReady(): boolean {
  return store.$state.viewPortHeight > 0 && store.$state.viewPortWidth > 0;
}

onMounted(async () => {
  await nextTick();
  if (viewport.value) {
    const height = viewport.value.getBoundingClientRect().height;
    store.$state.viewPortHeight = height;
  }
});
</script>

<template>
  <section :class="'w-full h-screen'">
    <ODSideBar />
    <div :class="'grid grid-rows-[auto_1fr_auto] h-full'">
      <ODHeader />
      <main :class="'flex-1 min-h-0'" ref="viewport">
        <!-- <section
          :class="'grid grid-cols-[auto_1fr] h-full'"
          v-if="viewPortSizeReady()"
        >
          <ODSideBar />
          <RouterView />
        </section> -->
        <RouterView v-if="viewPortSizeReady()" />
      </main>
      <ODFooter />
    </div>
  </section>
</template>
