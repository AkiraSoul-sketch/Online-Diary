import { useMediaQuery } from "@vueuse/core";
import { ref, watch, type Ref } from "vue";

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

  function isXS(): boolean {
    return type.value === MediaScreenType.XS;
  }

  function isSM(): boolean {
    return type.value === MediaScreenType.SM;
  }

  function isMD(): boolean {
    return type.value === MediaScreenType.MD;
  }

  function isLG(): boolean {
    return type.value === MediaScreenType.LG;
  }

  function isXL(): boolean {
    return type.value === MediaScreenType.XL;
  }

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
