import { computed, nextTick, onMounted, ref, type Ref } from "vue";
import { useCommonStore } from "../Stores/common.store";

export const useViewPortReadiness = () => {
  const viewport: Ref<HTMLElement | null> = ref(null);
  const store = useCommonStore();
  const ready = computed(() => {
    return store.$state.viewPortHeight > 0 && store.$state.viewPortWidth > 0;
  });

  onMounted(async () => {
    await nextTick();
    if (viewport.value) {
      const height = viewport.value.getBoundingClientRect().height;
      store.$state.viewPortHeight = height;
    }
  });

  return { viewport, ready };
};
