import { defineStore } from "pinia";
import { computed, ref, type Ref } from "vue";

export const useGlobalContainerStore = defineStore("globalContainer", () => {
  const widthValue: Ref<number> = ref(0);
  const heightValue: Ref<number> = ref(0);

  const width = computed(() => {
    return widthValue.value;
  });

  const height = computed(() => {
    return heightValue.value;
  });

  function adjustWidth(number: number): void {
    if (number === width.value || number < 0) return;
    widthValue.value = number;
  }

  function adjustHeight(number: number): void {
    if (number === height.value || number < 0) return;
    heightValue.value = number;
  }

  return {
    width,
    height,
    adjustWidth,
    adjustHeight,
  };
});
