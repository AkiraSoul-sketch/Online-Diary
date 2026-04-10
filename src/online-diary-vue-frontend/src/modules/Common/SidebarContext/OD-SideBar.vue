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
  DrawerFooter,
} from "@/components/ui/drawer";
import SidebarExitButton from "./components/SidebarExitButton.vue";
import type { FocusOutsideEvent } from "reka-ui";
import { Button } from "@/components/ui/button";
import { LogOutIcon } from "lucide-vue-next";
import { useAuthenticationStatusStore } from "../Authentication/authentication.status.store";

const width: Ref<number> = ref(0);
const common = useCommonStore();
const authenticationStatus = useAuthenticationStatusStore();

function focusedOutside(_: FocusOutsideEvent): void {
  common.toggleSideBar();
}

function logout(): void {
  authenticationStatus.logout();
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
  <Drawer :fixed="true" :open="common.$state.sideBarHidden" :no-body-styles="true" :direction="'left'">
    <DrawerContent @interact-outside="focusedOutside" data-vaul-no-drag :class="'card-primary'">
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
      <DrawerFooter>
        <Button v-on:click="logout" v-if="authenticationStatus.isLoggedIn" :size="'icon'" :variant="'secondary'"
          :class="'w-1/2 text-responsive-tertiary'">
          <LogOutIcon />
          <span>выход</span>
        </Button>
      </DrawerFooter>
    </DrawerContent>
  </Drawer>
</template>
