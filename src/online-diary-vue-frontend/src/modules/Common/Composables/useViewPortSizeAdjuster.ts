import { ref, watch, type Ref } from "vue";
import { useCommonStore } from "../Stores/common.store";
import { useWindowSize } from "@vueuse/core";
import { useElementSizeObservabilityV2 } from "./useElementSizeObservabilityV2";

/**
 * Composable for tracking viewport element readiness and synchronizing its height with the common store.
 *
 * @remarks
 * - Exposes a template ref (`viewport`) intended to be bound to a root/container DOM element.
 * - Computes readiness (`ready`) based on whether both viewport dimensions in the shared store are greater than zero.
 * - On component mount, waits for the next DOM update tick, then reads the bound element height via
 *   `getBoundingClientRect()` and writes it to `store.$state.viewPortHeight`.
 *
 * @returns An object containing:
 * - `viewport`: Reactive element reference used to access the viewport DOM node.
 * - `ready`: Reactive boolean indicating whether viewport width and height are initialized in the store.
 */
export const useViewPortSizeAdjuster = () => {
  const globalContainerWidthTracker = useElementSizeObservabilityV2();
  const commonStore = useCommonStore();
  const windowSize = useWindowSize();
  const globalContainer: Ref<HTMLElement | null> = ref(null);
  const height: Ref<number> = ref(0);
  const width: Ref<number> = ref(0);

  watch(
    () => [
      windowSize.height.value,
      commonStore.headerHeight,
      commonStore.footerHeight,
    ],
    ([windowHeight, headerHeight, footerHeight]) => {
      height.value = windowHeight - headerHeight - footerHeight;
      commonStore.adjustViewPortHeight(height.value);
    },
    {
      immediate: true,
    },
  );

  watch(
    () => globalContainer.value,
    (element) => {
      if (element) globalContainerWidthTracker.element.value = element;
    },
    {
      immediate: true,
    },
  );

  watch(
    () => globalContainerWidthTracker.width.value,
    ($width) => {
      commonStore.adjustViewPortWidth($width);
    },
    {
      immediate: true,
    },
  );

  return { globalContainer, height, width };
};
