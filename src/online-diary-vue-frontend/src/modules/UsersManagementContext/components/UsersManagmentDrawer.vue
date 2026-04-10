<script setup lang="ts">
import { ref } from "vue";
import Button from "@/components/ui/button/Button.vue";
import Input from "@/components/ui/input/Input.vue";
import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";
import { Drawer, DrawerContent, DrawerClose } from "@/components/ui/drawer";

const { isLG, isXL, isXXL } = useMediaScreenTypeTracker();
const wasd = defineEmits<{
  (e: "closed"): void;
}>();
const props = defineProps<{
  isOpen: boolean;
}>();
</script>

<template>
  <Drawer :open="isOpen" :no-body-styles="true" :direction="'right'" v-if="isLG() || isXL() || isXXL()">
    <DrawerContent :class="'item-bg-primary'">
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
