import { ref, watch, type Ref } from "vue";
import { useGlobalContainerStore } from "../Stores/globalContainer.store";
import { useElementSizeObservabilityV2 } from "./useElementSizeObservabilityV2";

// отслеживает ширину глобального контейнера и сохраняет её.
// использовать в самом начальном компоненте, например в App.vue.
export const useGlobalContainerWidthTracker = () => {
  const container: Ref<HTMLElement | null> = ref(null);
  const size = useElementSizeObservabilityV2();
  const store = useGlobalContainerStore();
  watch(() => size.width.value, update);
  watch(() => container.value, initializeContainer);
  function update(width: number): void {
    store.adjustWidth(width);
  }
  function initializeContainer(container: HTMLElement | null): void {
    size.element.value = container;
  }

  return { container };
};
