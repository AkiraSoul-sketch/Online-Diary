<script setup lang="ts">
import { ListFilterPlusIcon, PlusIcon } from "lucide-vue-next";
import { Button } from "@/components/ui/button";
import DisciplinesList_xs_sm_md from "./DisciplinesList_xs_sm_md.vue";
import DisciplinesList_lg from "./DisciplinesList_lg.vue";
import DisciplinesList_xl_xxl from "./DisciplinesList_xl_xxl.vue";
import VerticalScrollableContent from "@/modules/Common/Components/VerticalScrollableContent.vue";
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";
import { ref, watch, type Ref } from "vue";
import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";

const props = defineProps<{
  dataHeightLimit: number;
}>();
const header = useElementSizeObservabilityV2();
const heightLimit: Ref<number> = ref(0);
const { isXS, isSM, isMD } = useMediaScreenTypeTracker();

function isMobile(): boolean {
  return isXS() || isSM() || isMD();
}

const emits = defineEmits<{
  (e: 'open-mobile-filters'): void;
}>();

watch(
  () => [props.dataHeightLimit, header.height.value],
  ([$dataHeightLimit, $header]) => {
    heightLimit.value = $dataHeightLimit - $header;
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <section :class="'flex-column-layout full-size card-primary rounded-md border'">
    <div :ref="header.element" :class="'p-4 item-bg-secondary flex-column-layout rounded-t-md'">
      <div :class="'text-responsive-primary font-semibold font-prussian-blue'">
        Список дисциплин
      </div>
      <div :class="'flex-row-layout gap-2'">
        <div class="flex items-center gap-2 ml-auto">
          <Button v-on:click="emits('open-mobile-filters')" v-if="isMobile()" :variant="'primary'"
            class=" flex items-center gap-2">
            <ListFilterPlusIcon :size="16" />
            <span class="text-responsive-secondary">Фильтры</span>
          </Button>
          <Button :variant="'primary'" class=" flex items-center gap-2">
            <PlusIcon :size="16" />
            <span class="text-responsive-secondary">Создать</span>
          </Button>
        </div>
      </div>
    </div>
    <VerticalScrollableContent :class="'item-bg-primary'" :height-limit="heightLimit">
      <DisciplinesList_xs_sm_md />
      <DisciplinesList_lg />
      <DisciplinesList_xl_xxl />
    </VerticalScrollableContent>
  </section>
</template>
