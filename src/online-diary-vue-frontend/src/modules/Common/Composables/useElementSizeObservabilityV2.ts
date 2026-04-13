import { computed, onUnmounted, ref, watch, type Ref } from "vue";
import type { ElementSize } from "./useElementSizeObservability";

// хук для отслеживания изменения ширины и высоты html элемента.
// не работает с компонентами.
export function useElementSizeObservabilityV2(loggingOptions?: LoggingOptions) {
  const $size: Ref<ElementSize> = ref(createEmpty());
  const element: Ref<HTMLElement | null> = ref(null);
  let observer: ResizeObserver | null = null as ResizeObserver | null;
  let cachedElement: HTMLElement | null = null;

  const width = computed(() => $size.value.width);
  const height = computed(() => $size.value.height);

  function observe(el: HTMLElement): void {
    element.value = el;
  }

  // наблюдать за изменениями размера элемента
  watch(
    () => element.value,
    ($element) => {
      const resolved = resolveHTMLElement($element);
      if (cachedElement === resolved) return;
      observer?.disconnect();
      observer = null;
      if (resolved) observer = createObserver(resolved, $size);
    },
  );

  // логировать
  watch(
    $size,
    (size) => {
      logSizeIfEnabled(loggingOptions, size);
    },
    { deep: true },
  );

  // нужно, чтобы контролировать ресурсы
  onUnmounted(() => {
    observer?.disconnect();
  });

  return { element, width, height, observe };
}

type LoggingOptions = {
  alias: string;
  useLogging: boolean;
};

function resolveHTMLElement(
  elementRef: HTMLElement | null,
): HTMLElement | null {
  if (!elementRef) return null;
  if (elementRef instanceof HTMLElement) return resolveFromHTML(elementRef);
  return null;
}

function resolveFromHTML(element: HTMLElement): HTMLElement {
  return element;
}

function createObserver(
  element: HTMLElement,
  size: Ref<ElementSize>,
): ResizeObserver {
  const observer = new ResizeObserver((entries) => {
    for (const entry of entries) {
      if (entry.target !== element) continue;
      updateSize(size, entry);
    }
  });

  observer.observe(element);
  return observer;
}

function updateSize(size: Ref<ElementSize>, entry: ResizeObserverEntry): void {
  const box = Array.isArray(entry.contentBoxSize)
    ? entry.contentBoxSize[0]
    : entry.contentBoxSize;

  const height = box?.blockSize ?? entry.contentRect.height;
  const width = box?.inlineSize ?? entry.contentRect.width;
  size.value = { height, width };
}

function createEmpty(): ElementSize {
  return { height: 0, width: 0 };
}

function logSizeIfEnabled(
  options: LoggingOptions | undefined,
  size: ElementSize,
): void {
  if (options === undefined) return;
  if (!options.useLogging) return;
  console.log(`${options.alias} width changed: ${size.width}`);
  console.log(`${options.alias} height changed: ${size.height}`);
}
