import { defineStore } from "pinia";
import { ref, watch, type Ref } from "vue";
import { useCommonStore } from "../Common/Stores/common.store";

const useAdminSizeStore = defineStore("adminSize", () => {
  const commonStore = useCommonStore();
  const heightLimit: Ref<number> = ref(0);
  const headerHeight: Ref<number> = ref(0);

  watch(
    () => [commonStore.viewPortHeight, headerHeight.value],
    ([$viewPortHeight, $adminHeaderHeight]) => {
      const gaps: number = 12;
      const limit: number = $viewPortHeight - $adminHeaderHeight - gaps;
      heightLimit.value = limit;
    },
    {
      immediate: true,
      flush: "post",
    },
  );

  return { headerHeight, heightLimit };
});

export { useAdminSizeStore };
