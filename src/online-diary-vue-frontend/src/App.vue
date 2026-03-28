<script setup lang="ts">
import ODHeader from "./modules/Common/HeaderContext/OD-Header.vue";
import { nextTick, onMounted, ref, type Ref } from "vue";
import ODFooter from "./modules/Common/FooterContext/OD-Footer.vue";
import { useCommonStore } from "./common.store";

const viewport: Ref<HTMLElement | null> = ref(null);
const store = useCommonStore();

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
    <div :class="'grid grid-rows-[auto_1fr_auto] h-full'">
      <ODHeader />
      <main :class="'flex-1 min-h-0 p-1'" ref="viewport">
        <RouterView v-if="store.$state.viewPortHeight > 0" />
      </main>
      <ODFooter />
    </div>
  </section>
</template>
