<script setup lang="ts">
import { ref } from "vue";
import Button from "@/components/ui/button/Button.vue";
import Input from "@/components/ui/input/Input.vue";
import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";
import { Drawer, DrawerContent, DrawerClose } from "@/components/ui/drawer";
import type { FocusOutsideEvent, PointerDownOutsideEvent } from "reka-ui";

const { isXS, isSM, isMD } = useMediaScreenTypeTracker();
const isSelected = ref<boolean>(false);
const wasd = defineEmits<{
  (e: "closed"): void;
}>();
const props = defineProps<{
  isOpen: boolean;
}>();
function Handleoutsideclic(event: PointerDownOutsideEvent | FocusOutsideEvent) {
  isSelected.value = false;
}
</script>

<template>
  <!--Mobile version-->
  <Drawer
    :open="isOpen"
    :no-body-styles="true"
    :direction="'bottom'"
    v-if="isXS() || isSM() || isMD()"
  >
    <DrawerContent
      @interact-outside="Handleoutsideclic"
      :class="'item-bg-primary'"
    >
      <div class="flex flex-col gap-4 p-4">
        <Input type="FIO" placeholder="ФИО" />
        <Input type="login" placeholder="Логин" />
        <Input type="email" placeholder="Почта" />
        <Input type="Role" placeholder="Роль" />
      </div>
      <DrawerClose>
        <div class="flex p-4 justify-center">
          <Button v-on:click="wasd('closed')"> Закрыть </Button>
        </div>
      </DrawerClose>
    </DrawerContent>
  </Drawer>
</template>
