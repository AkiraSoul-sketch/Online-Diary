import { defineStore } from "pinia";
import { ref, type Ref } from "vue";

export const useCommonStore = defineStore("common", () => {
  const viewPortWidth: Ref<number> = ref(window.innerWidth);
  const viewPortHeight: Ref<number> = ref(window.innerHeight);
  const sideBarHidden: Ref<boolean> = ref(false);
  const headerHeight: Ref<number> = ref(0);
  const isAfterLogin: Ref<boolean> = ref(false);

  /**
   * Обновляет текущее значение ширины области просмотра.
   *
   * @param number - Новое значение ширины viewport в пикселях.
   * @returns Ничего не возвращает.
   */
  function adjustWidth(number: number): void {
    viewPortWidth.value = number;
  }

  /**
   * Обновляет значение высоты области просмотра.
   *
   * @param number - Новая высота viewport в пикселях.
   * @returns Ничего не возвращает.
   */
  function adjustHeight(number: number): void {
    viewPortHeight.value = number;
  }

  /**
   * Изменяет высоту заголовка, обновляя значение `headerHeight`.
   *
   * @param number Новое значение высоты заголовка.
   * @returns Ничего не возвращает.
   */
  function adjustHeaderHeight(number: number): void {
    headerHeight.value = number;
  }

  /**
   * Переключает состояние видимости боковой панели.
   *
   * Инвертирует текущее значение `sideBarHidden.value`:
   * - `true` → `false`
   * - `false` → `true`
   */
  function toggleSideBar(): void {
    sideBarHidden.value = !sideBarHidden.value;
  }

  /**
   * Устанавливает значение флага, указывающего, что состояние приложения соответствует этапу после входа пользователя в систему.
   *
   * @param value Логическое значение, определяющее состояние флага после авторизации.
   * @returns Ничего не возвращает.
   */
  function setIsAfterLoginV2(value: boolean): void {
    isAfterLogin.value = value;
  }

  return {
    viewPortHeight,
    viewPortWidth,
    sideBarHidden,
    headerHeight,
    isAfterLogin,
    setIsAfterLoginV2,
    adjustHeight,
    adjustWidth,
    toggleSideBar,
    adjustHeaderHeight,
  };
});
