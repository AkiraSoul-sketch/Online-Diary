import { ref, watch, type Ref } from "vue";
import { useAdminSizeStore } from "../adminSize.store";
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";

const useAdminSizeHeaderHeightTracker = () => {
  const adminSizeStore = useAdminSizeStore();
  const adminHeader: Ref<HTMLElement | null> = ref(null);
  const adminHeaderTracker = useElementSizeObservabilityV2();

  watch(
    () => adminHeader.value,
    ($header) => {
      if ($header) adminHeaderTracker.observe($header);
    },
    {
      immediate: true,
      flush: "post",
    },
  );

  watch(
    () => adminHeaderTracker.height.value,
    ($height) => {
      adminSizeStore.headerHeight = Math.floor($height);
    },
    {
      immediate: true,
      flush: "post",
    },
  );

  return { adminHeader };
};

export { useAdminSizeHeaderHeightTracker };
