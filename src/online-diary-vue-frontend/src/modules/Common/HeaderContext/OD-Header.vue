<script setup lang="ts">
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { FieldContent, Field } from "@/components/ui/field";
import { InfoIcon } from "lucide-vue-next";
import InputWithIcon from "../Components/InputWithIcon.vue";
import { useCommonStore } from "@/common.store";

const common = useCommonStore();

const props = defineProps<{
  sideBarPanelWidth?: number;
}>();
</script>

<template>
  <header
    :class="'flex w-full p-1 justify-around  gap-2 items-center bg-(--header-background-color)'"
  >
    <img
      src="/main_logo.svg"
      :class="'h-8 brightness-0 sm:h-9 md:h-11 lg:h-13 xl:h-15 2xl:h-17'"
      v-on:click="common.toggleSideBar"
    />
    <InputWithIcon :place-holder="'Поиск...'" :icon="InfoIcon" />
    <Avatar
      :class="'shadow-(--shadow-basic) h-8 w-8 sm:h-9 sm:w-9 md:h-11 md:w-11 lg:h-13 lg:w-13 xl:h-15 xl:w-15 2xl:h-17 2xl:w-17'"
    >
      <AvatarFallback>AP</AvatarFallback>
    </Avatar>
    <FieldContent :class="'gap-0'">
      <Field :class="'text-responsive-tertiary text-nowrap'"
        >Проничкин Д.С.</Field
      >
      <Field :class="'text-responsive-tertiary'">преподаватель</Field>
    </FieldContent>
  </header>
</template>
