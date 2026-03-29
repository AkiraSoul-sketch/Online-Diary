<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import ScrollableContent from "@/modules/Common/Components/ScrollableContent.vue";
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";
import { ref, watch, type Ref } from "vue";
import AdminActivityCard from "./AdminActivityCard.vue";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import { SearchIcon } from "lucide-vue-next";

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
  <div :class="'gap-6 p-2 my-2'" :ref="containerSizeObserver.element">
    <Card :class="'shadow-none flex-1 min-h-0 flex-col gap-6 p-0 border-none'">
      <div :ref="titleSizeObserver.element">
        <CardTitle>Список действий</CardTitle>
      </div>
      <div :class="'px-2'" :ref="inputObserver.element">
        <InputWithIcon :place-holder="'Фильтр по тексту'" :icon="SearchIcon" />
      </div>
      <CardContent
        :class="'flex-1 min-h-0 shadow-(--shadow-basic) rounded-sm p-2'"
      >
        <ScrollableContent :height-limit="scrollAreaLimit">
          <AdminActivityCard v-for="value in Array(51)" />
        </ScrollableContent>
      </CardContent>
    </Card>
  </div>
</template>
