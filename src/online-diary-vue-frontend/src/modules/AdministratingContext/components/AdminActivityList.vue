<script setup lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import { ref, watch, type Ref } from "vue";
import AdminActivityCard from "./AdminActivityCard.vue";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import { SearchIcon } from "lucide-vue-next";
import VerticalScrollableContent from "@/modules/Common/Components/VerticalScrollableContent.vue";
import { useAdminSizeStore } from "../adminSize.store";

const scrollAreaLimit: Ref<number> = ref(0);
const adminSizeStore = useAdminSizeStore();

watch(
  () => adminSizeStore.heightLimit,
  ($heightLimit) => {
    scrollAreaLimit.value = $heightLimit;
  },
  {
    immediate: true,
    flush: 'post',
  }
);

</script>

<template>
  <div :class="'flex flex-col gap-6 p-2 my-2 card-primary rounded-sm w-full'">
    <div>
      <CardTitle :class="'font-normal text-responsive-primary'">Список действий</CardTitle>
    </div>
    <Card :class="'item-bg-primary gap-6 p-0 borderless shadow-none'">
      <div :class="'px-2'">
        <InputWithIcon :class="'text-responsive-secondary'" :place-holder="'Фильтр по тексту'" :icon="SearchIcon" />
      </div>
      <CardContent :class="'rounded-sm p-2'">
        <VerticalScrollableContent :height-limit="scrollAreaLimit">
          <AdminActivityCard v-for="value in Array(51)" />
        </VerticalScrollableContent>
      </CardContent>
    </Card>
  </div>
</template>