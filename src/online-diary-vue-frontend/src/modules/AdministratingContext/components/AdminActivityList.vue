<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import ScrollableContent from "@/modules/Common/Components/ScrollableContent.vue";
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";
import { ref, watch, type Ref } from "vue";
import AdminActivityCard from "./AdminActivityCard.vue";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import { SearchIcon } from "lucide-vue-next";
import VerticalScrollableContent from "@/modules/Common/Components/VerticalScrollableContent.vue";

const scrollAreaLimit: Ref<number> = ref(0);
const containerSizeObserver = useElementSizeObservabilityV2();
const titleSizeObserver = useElementSizeObservabilityV2();
const inputObserver = useElementSizeObservabilityV2();
watch(
  () => [
    containerSizeObserver.height.value,
    titleSizeObserver.height.value,
    inputObserver.height.value,
  ],
  ([$containerHeight, $titleHeight, $inputHeight]) => {
    scrollAreaLimit.value = $containerHeight - $inputHeight - $titleHeight - 72;
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <div
    :class="'gap-6 p-2 my-2 bg-block-light-neutral shadow-(--shadow-basic) rounded-sm'"
    :ref="containerSizeObserver.element"
  >
    <Card :class="'flex-1 min-h-0 flex-col gap-6 p-0 border-none shadow-none'">
      <div :ref="titleSizeObserver.element">
        <CardTitle>Список действий</CardTitle>
      </div>
      <div :class="'px-2'" :ref="inputObserver.element">
        <InputWithIcon :place-holder="'Фильтр по тексту'" :icon="SearchIcon" />
      </div>
      <CardContent :class="'flex-1 min-h-0 rounded-sm p-2'">
        <VerticalScrollableContent :height-limit="scrollAreaLimit">
          <AdminActivityCard v-for="value in Array(51)" />
        </VerticalScrollableContent>
      </CardContent>
    </Card>
  </div>
</template>
