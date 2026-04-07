import { useMediaQuery } from "@vueuse/core";
import { ref, watch, type Ref } from "vue";

/**
 * Перечисление категорий размеров экрана для адаптивной вёрстки.
 *
 * Используется для определения текущего брейкпоинта интерфейса
 * и применения соответствующих стилей/поведения в зависимости
 * от ширины области просмотра.
 *
 * Значения идут в порядке возрастания размера экрана:
 * `XS` → `SM` → `MD` → `LG` → `XL` → `XXL`.
 */
export enum MediaScreenType {
  XS,
  SM,
  MD,
  LG,
  XL,
  XXL,
}

const useMediaScreenTypeTracker = () => {
  const type: Ref<MediaScreenType> = ref(MediaScreenType.XS);

  const _isXS = useMediaQuery("(max-width: 639px)");
  const _isSM = useMediaQuery("(min-width: 640px) and (max-width: 767px)");
  const _isMD = useMediaQuery("(min-width: 768px) and (max-width: 1023px)");
  const _isLG = useMediaQuery("(min-width: 1024px) and (max-width: 1279px)");
  const _isXL = useMediaQuery("(min-width: 1280px) and (max-width: 1535px)");
  const _isXXL = useMediaQuery("(min-width: 1536px)");

  /**
   * Проверяет, соответствует ли текущий тип экрана размеру **XS**.
   *
   * @returns `true`, если текущий тип экрана — {@link MediaScreenType.XS}; иначе `false`.
   */
  function isXS(): boolean {
    return type.value === MediaScreenType.XS;
  }

  /**
   * Проверяет, соответствует ли текущий тип экрана размеру `SM`.
   *
   * @returns `true`, если `type.value` равен {@link MediaScreenType.SM}, иначе `false`.
   */
  function isSM(): boolean {
    return type.value === MediaScreenType.SM;
  }

  /**
   * Проверяет, соответствует ли текущий размер экрана типу MD (средний размер).
   * @returns {boolean} `true`, если размер экрана равен MediaScreenType.MD, иначе `false`
   */
  function isMD(): boolean {
    return type.value === MediaScreenType.MD;
  }

  /**
   * Проверяет, соответствует ли текущий тип экрана значению `LG` (большой экран).
   *
   * Используйте этот метод для условной логики интерфейса,
   * которая должна применяться только на больших экранах.
   *
   * @returns `true`, если `type.value` равен `MediaScreenType.LG`; иначе `false`.
   */
  function isLG(): boolean {
    return type.value === MediaScreenType.LG;
  }

  /**
   * Проверяет, находится ли экран в категории XL (extra large).
   * @returns {boolean} `true`, если текущий тип экрана является XL, иначе `false`
   */
  function isXL(): boolean {
    return type.value === MediaScreenType.XL;
  }

  /**
   * Проверяет, находится ли экран в разрешении XXL
   * @returns {boolean} true, если текущее разрешение экрана равно XXL, иначе false
   */
  function isXXL(): boolean {
    return type.value === MediaScreenType.XXL;
  }

  watch(
    () => [
      _isXS.value,
      _isSM.value,
      _isMD.value,
      _isLG.value,
      _isXL.value,
      _isXXL.value,
    ],
    ([xs, sm, md, lg, xl, xxl]) => {
      if (xs) {
        type.value = MediaScreenType.XS;
        return;
      }
      if (sm) {
        type.value = MediaScreenType.SM;
        return;
      }
      if (md) {
        type.value = MediaScreenType.MD;
        return;
      }
      if (lg) {
        type.value = MediaScreenType.LG;
        return;
      }
      if (xl) {
        type.value = MediaScreenType.XL;
        return;
      }
      if (xxl) {
        type.value = MediaScreenType.XXL;
        return;
      }
    },
    {
      immediate: true,
    },
  );

  return {
    type,
    isXS,
    isSM,
    isMD,
    isLG,
    isXL,
    isXXL,
  };
};

export { useMediaScreenTypeTracker };
