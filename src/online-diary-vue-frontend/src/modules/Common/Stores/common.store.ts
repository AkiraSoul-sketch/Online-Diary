import { defineStore } from "pinia";
import { ref, type Ref } from "vue";

export const useCommonStore = defineStore("common", () => {
  const viewPortWidth: Ref<number> = ref(window.innerWidth);
  const viewPortHeight: Ref<number> = ref(window.innerHeight);
  const sideBarHidden: Ref<boolean> = ref(false);
  const headerHeight: Ref<number> = ref(0);

  function adjustWidth(number: number): void {
    viewPortWidth.value = number;
  }

  function adjustHeight(number: number): void {
    viewPortHeight.value = number;
  }

  function adjustHeaderHeight(number: number): void {
    headerHeight.value = number;
  }

  function toggleSideBar(): void {
    sideBarHidden.value = !sideBarHidden.value;
  }

  return {
    viewPortHeight,
    viewPortWidth,
    sideBarHidden,
    headerHeight,
    adjustHeight,
    adjustWidth,
    toggleSideBar,
    adjustHeaderHeight,
  };
});
