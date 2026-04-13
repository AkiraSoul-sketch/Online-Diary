import { useWindowSize } from "@vueuse/core";
import { computed, ref, type Ref } from "vue";

export const useDisciplinesViewManager = () => {
  const filtersDrawerOpen: Ref<boolean> = ref(false);
  const createDrawerOpen: Ref<boolean> = ref(false);
  const windowSize = useWindowSize();

  const canRenderTable = computed(() => {
    const width: number = windowSize.width.value;
    const canRender: boolean = width >= 1250;
    console.log(canRender);
    return canRender;
  });

  const isCreateDrawerOpen = computed(() => {
    return createDrawerOpen.value;
  });

  const isFiltersDrawerOpen = computed(() => {
    return filtersDrawerOpen.value;
  });

  function toggleCreateDrawer(): void {
    createDrawerOpen.value = !createDrawerOpen.value;
  }

  function toggleFiltersDrawer(): void {
    filtersDrawerOpen.value = !filtersDrawerOpen.value;
  }

  return {
    canRenderTable,
    isFiltersDrawerOpen,
    isCreateDrawerOpen,
    toggleFiltersDrawer,
    toggleCreateDrawer,
  };
};
