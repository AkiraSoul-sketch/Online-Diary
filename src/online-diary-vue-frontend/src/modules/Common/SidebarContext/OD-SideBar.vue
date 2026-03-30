<script setup lang="ts">
import ODSidebarMenuContent from "./components/OD-SidebarMenuContent.vue";
import ODSidebarAvatar from "./components/OD-SidebarAvatar.vue";
import { useCommonStore } from "@/modules/Common/Stores/common.store";
import { ref, watch, type Ref } from "vue";
import { XIcon } from "lucide-vue-next";
import {
  Drawer,
  DrawerContent,
  DrawerHeader,
  DrawerTitle,
  DrawerClose,
  DrawerDescription,
} from "@/components/ui/drawer";
import { Button } from "@/components/ui/button";
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
  <Drawer
    :open="common.$state.sideBarHidden"
    :no-body-styles="true"
    :direction="'left'"
  >
    <DrawerContent @interact-outside="focusedOutside" :class="'bg-accent'">
      <DrawerHeader :class="'flex flex-row justify-between items-center h-20'">
        <DrawerTitle :class="'flex items-center gap-5'">
          <img src="/main_logo.svg" :class="'h-12 brightness-0'" />
          Меню
        </DrawerTitle>
        <DrawerClose>
          <Button
            v-on:click="common.toggleSideBar"
            :class="'border rounded-2xl w-9 shadow-(--shadow-basic)'"
          >
            <XIcon />
          </Button>
        </DrawerClose>
      </DrawerHeader>
      <DrawerDescription>
        <ODSidebarAvatar />
        <ODSidebarMenuContent />
      </DrawerDescription>
    </DrawerContent>
  </Drawer>
</template>
