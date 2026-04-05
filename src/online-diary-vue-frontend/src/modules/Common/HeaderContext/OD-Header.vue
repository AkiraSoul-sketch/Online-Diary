<script setup lang="ts">
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { FieldContent, Field } from "@/components/ui/field";
import { InfoIcon } from "lucide-vue-next";
import InputWithIcon from "../Components/InputWithIcon.vue";
import { useCommonStore } from "@/modules/Common/Stores/common.store";
import { useElementSizeObservabilityV2 } from "../Composables/useElementSizeObservabilityV2";
import { watch } from "vue";

const common = useCommonStore();
const header = useElementSizeObservabilityV2();

const props = defineProps<{
  sideBarPanelWidth?: number;
}>();

watch(
  () => header.height.value,
  ($height) => {
    common.adjustHeaderHeight($height);
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <header :ref="header.element"
    :class="'flex w-full p-1 card-sixth-accent justify-around gap-2 items-center bg-(--header-background-color)'">
    <img src="/main_logo.svg" :class="'h-8 brightness-0 sm:h-9 md:h-11 lg:h-13 xl:h-15 2xl:h-17'"
      v-on:click="common.toggleSideBar" />
    <InputWithIcon :place-holder="'Поиск...'" :icon="InfoIcon" />
    <Avatar
      :class="'shadow-(--shadow-basic) h-8 w-8 sm:h-9 sm:w-9 md:h-10 md:w-10 lg:h-11 lg:w-11 xl:h-13 xl:w-13 2xl:h-15 2xl:w-15'">
      <AvatarFallback :class="'text-responsive-tertiary'">ДС</AvatarFallback>
    </Avatar>
    <FieldContent :class="'gap-0'">
      <Field :class="'text-responsive-tertiary text-nowrap'">Проничкин Д.С.</Field>
      <Field :class="'text-responsive-tertiary'">преподаватель</Field>
    </FieldContent>
  </header>
</template>
