import { defineStore } from "pinia";
import { computed, ref, type Ref } from "vue";

export const useGlobalContainerStore = defineStore("globalContainer", () => {
  const widthValue: Ref<number> = ref(0);
  const heightValue: Ref<number> = ref(0);

  /**
   * Вычисляемое свойство, возвращающее текущее значение ширины контейнера страницы (глобального контейнера).
   *
   * @remarks
   * Оборачивает реактивное значение `widthValue.value` в `computed`,
   * чтобы обеспечить кэшируемый и автоматически обновляемый доступ к ширине.
   *
   * @returns Текущее числовое значение ширины.
   */
  const width = computed(() => {
    return widthValue.value;
  });

  /**
   * Реактивно вычисляемое значение текущей высоты контейнера страницы (глобального контейнера).
   *
   * Возвращает актуальное значение из `heightValue`, чтобы при его изменении
   * зависимые части интерфейса обновлялись автоматически.
   *
   * @returns Текущее значение высоты.
   */
  const height = computed(() => {
    return heightValue.value;
  });

  /**
   * Обновляет значение ширины контейнера, если передано корректное новое значение.
   *
   * @param number Новое значение ширины.
   * @returns Ничего не возвращает.
   *
   * @remarks
   * Если переданное значение совпадает с текущей шириной
   * или является отрицательным, обновление не выполняется.
   */
  function adjustWidth(number: number): void {
    if (number === width.value || number < 0) return;
    widthValue.value = number;
  }

  /**
   * Обновляет значение высоты, если переданное значение отличается от текущего
   * и не является отрицательным.
   *
   * @param number Новое значение высоты.
   * @returns Ничего не возвращает.
   */
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
