<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import { TableRow } from "@/components/ui/table";
import ScrollableContent from "@/modules/Common/Components/ScrollableContent.vue";
import { useElementSizeObservability } from "@/modules/Common/Composables/useElementSizeObservability";
import { ref, watch, type Ref } from "vue";

const container: Ref<HTMLElement | null> = ref(null);
const title: Ref<HTMLElement | null> = ref(null);
const scrollAreaLimit: Ref<number> = ref(0);
const containerSize = useElementSizeObservability(container);
const titleSize = useElementSizeObservability(title);
watch(
  () => [containerSize.value.height, titleSize.value.height],
  ([$containerHeight, $titleHeight]) => {
    scrollAreaLimit.value = $containerHeight - $titleHeight - 36;
    console.log(scrollAreaLimit.value);
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <div :class="'flex-1 h-full flex flex-col'" ref="container">
    <Card :class="'flex-1 min-h-0 flex-col gap-1 p-2'">
      <div ref="title">
        <CardTitle>Список действий</CardTitle>
      </div>
      <CardContent
        :class="'flex-1 min-h-0 shadow-(--shadow-basic) rounded-sm p-2'"
      >
        <ScrollableContent :height-limit="scrollAreaLimit">
          <TableRow v-for="value in Array(50)"> 1231231 </TableRow>
        </ScrollableContent>
      </CardContent>
    </Card>
  </div>
</template>
