<script setup lang="ts">
import ODSidebarMenuContent from "./components/OD-SidebarMenuContent.vue";
import ODSidebarAvatar from "./components/OD-SidebarAvatar.vue";
import { useCommonStore } from "@/modules/Common/Stores/common.store";
import { ref, watch, type Ref } from "vue";
import {
  Drawer,
  DrawerContent,
  DrawerHeader,
  DrawerTitle,
  DrawerClose,
  DrawerDescription,
} from "@/components/ui/drawer";
import SidebarExitButton from "./components/SidebarExitButton.vue";
import DrawerOverlay from "@/components/ui/drawer/DrawerOverlay.vue";

const width: Ref<number> = ref(0);
const common = useCommonStore();

watch(
  () => common.$state.viewPortWidth,
  ($vpWidth) => {
    const newWidth = $vpWidth / 6;
    width.value = newWidth;
  },
  {
    immediate: true,
  },
);
</script>
<template>
  <Drawer
    :open="common.$state.sideBarHidden"
    :no-body-styles="true"
    :direction="'left'"
    :dismissible="false"
  >
    <DrawerContent :class="'bg-accent'">
      <DrawerHeader :class="'flex flex-row justify-between items-center h-20'">
        <DrawerTitle :class="'flex items-center gap-5'">
          <img src="/main_logo.svg" :class="'h-12 brightness-0'" />
          Меню
        </DrawerTitle>
        <DrawerClose>
          <SidebarExitButton />
        </DrawerClose>
      </DrawerHeader>
      <DrawerDescription>
        <ODSidebarAvatar />
        <ODSidebarMenuContent />
      </DrawerDescription>
    </DrawerContent>
  </Drawer>
</template>
