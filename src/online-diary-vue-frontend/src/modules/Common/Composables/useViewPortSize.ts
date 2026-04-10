import { computed, nextTick, onMounted, ref, watch, type Ref } from "vue";
import { useCommonStore } from "../Stores/common.store";
import { useWindowSize } from "@vueuse/core";

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
export const useViewPortSize = () => {
  const commonStore = useCommonStore();
  const windowSize = useWindowSize();
  const _height: Ref<number> = ref(0);
  const _width: Ref<number> = ref(0);
  const height = computed(() => _height.value);
  const width = computed(() => _width.value);

  watch(
    () => [commonStore.headerHeight, commonStore.footerHeight],
    ([headerHeight, footerHeight]) => {
      _height.value = windowSize.height.value - headerHeight - footerHeight;
      console.log("Viewport height updated:", _height.value);
    },
  );

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

  return { viewport, ready, height, width };
};
