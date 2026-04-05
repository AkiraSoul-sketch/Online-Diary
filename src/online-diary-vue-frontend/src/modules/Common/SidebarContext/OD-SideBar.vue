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
import type { FocusOutsideEvent } from "reka-ui";

const width: Ref<number> = ref(0);
const common = useCommonStore();
function focusedOutside(_: FocusOutsideEvent): void {
  common.toggleSideBar();
}

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
  <Drawer :open="common.$state.sideBarHidden" :no-body-styles="true" :direction="'left'">
    <DrawerContent @interact-outside="focusedOutside" :class="'bg-accent'">
      <DrawerHeader :class="'item-bg-quaternary flex-row-layout justify-between items-center h-20'">
        <DrawerTitle :class="'text-responsive-primary flex items-center gap-5'">
          <img src="/main_logo.svg" :class="'h-12 brightness-0'" />
          Меню
        </DrawerTitle>
        <DrawerClose>
          <SidebarExitButton />
        </DrawerClose>
      </DrawerHeader>
      <div :class="'item-bg-quaternary'">
        <ODSidebarAvatar />
      </div>
      <DrawerDescription>
        <ODSidebarMenuContent />
      </DrawerDescription>
    </DrawerContent>
  </Drawer>
</template>
