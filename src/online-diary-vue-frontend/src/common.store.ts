import { defineStore } from "pinia";
import { ref, type Ref } from "vue";

export const useCommonStore = defineStore("common", () => {
  const viewPortWidth: Ref<number> = ref(window.innerWidth);
  const viewPortHeight: Ref<number> = ref(window.innerHeight);

  function adjustWidth(number: number): void {
    viewPortWidth.value = number;
  }

  function adjustHeight(number: number): void {
    viewPortHeight.value = number;
  }

  return { viewPortHeight, viewPortWidth, adjustHeight, adjustWidth };
});
