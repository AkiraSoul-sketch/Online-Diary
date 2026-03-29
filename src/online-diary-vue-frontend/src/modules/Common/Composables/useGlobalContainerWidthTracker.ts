import { ref, watch, type Ref } from "vue";
import { useElementSizeObservability } from "./useElementSizeObservability";
import { useGlobalContainerStore } from "../Stores/globalContainer.store";

export const useGlobalContainerWithTracker = () => {
  const container: Ref<HTMLElement | null> = ref(null);
  const size = useElementSizeObservability(container);
  const store = useGlobalContainerStore();
  watch(() => size.value.width, update);
  function update(width: number): void {
    console.log("updated");
    store.adjustWidth(width);
  }
  return { container };
};
