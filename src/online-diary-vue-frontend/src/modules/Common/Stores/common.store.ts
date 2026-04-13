import { defineStore } from "pinia";
import { ref, type Ref } from "vue";

export const useCommonStore = defineStore("common", () => {
  const viewPortWidth: Ref<number> = ref(window.innerWidth);
  const viewPortHeight: Ref<number> = ref(window.innerHeight);
  const sideBarHidden: Ref<boolean> = ref(false);
  const headerHeight: Ref<number> = ref(0);
  const footerHeight: Ref<number> = ref(0);
  const isAfterLogin: Ref<boolean> = ref(false);

  /**
   * Обновляет текущее значение ширины области просмотра.
   *
   * @param input - Новое значение ширины viewport в пикселях.
   * @returns Ничего не возвращает.
   */
  function adjustViewPortWidth(input: number): void {
    if (input < 0) return;
    viewPortWidth.value = input;
  }

  /**
   * Обновляет значение высоты области просмотра.
   *
   * @param input - Новая высота viewport в пикселях.
   * @returns Ничего не возвращает.
   */
  function adjustViewPortHeight(input: number): void {
    if (input < 0) return;
    viewPortHeight.value = input;
  }

  /**
   * Изменяет высоту заголовка, обновляя значение `headerHeight`.
   *
   * @param input Новое значение высоты заголовка.
   * @returns Ничего не возвращает.
   */
  function adjustHeaderHeight(input: number): void {
    if (input < 0) return;
    headerHeight.value = input;
  }

  /**
   * Изменяет высоту нижнего колонтитула (footer), обновляя значение `footerHeight`.
   *
   * @param input Новая высота footer в пикселях. Если значение отрицательное, операция игнорируется.
   * @returns Ничего не возвращает.
   */
  function adjustFooterHeight(input: number): void {
    if (input < 0) return;
    footerHeight.value = input;
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
    headerHeight,
    footerHeight,
    sideBarHidden,
    isAfterLogin,
    setIsAfterLoginV2,
    toggleSideBar,
    adjustHeaderHeight,
    adjustFooterHeight,
    adjustViewPortHeight,
    adjustViewPortWidth,
  };
});
