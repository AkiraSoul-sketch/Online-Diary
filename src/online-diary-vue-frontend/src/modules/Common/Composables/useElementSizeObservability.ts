import { onUnmounted, ref, watch, type Ref } from "vue";

export type ElementSize = {
  height: number;
  width: number;
};

type LoggingOptions = {
  alias: string;
  useLogging: boolean;
};

export function getObservedElementHeight(size: {
  size: Ref<ElementSize, ElementSize>;
}): number {
  return size.size.value.height;
}

export function getObservedElementWidth(size: {
  size: Ref<ElementSize, ElementSize>;
}): number {
  return size.size.value.width;
}

export function useElementSizeObservability(
  elementRef: Ref<HTMLElement | null>,
  loggingOptions?: LoggingOptions,
) {
  const size: Ref<ElementSize> = ref(createEmpty());
  let observer: ResizeObserver | null = null as ResizeObserver | null;

  // наблюдать за изменениями размера элемента
  watch(
    () => elementRef.value,
    (element) => {
      observer?.disconnect();
      observer = null;
      const resolved: HTMLElement | null = resolveHTMLElement(element);
      if (resolved) observer = createObserver(resolved, size);
    },
    { immediate: true },
  );

  // логировать
  watch(
    () => size.value,
    (v) => {
      logSizeIfEnabled(loggingOptions, v);
    },
  );

  // нужно, чтобы контролировать ресурсы
  onUnmounted(() => {
    observer?.disconnect();
  });

  return { size };
}

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
