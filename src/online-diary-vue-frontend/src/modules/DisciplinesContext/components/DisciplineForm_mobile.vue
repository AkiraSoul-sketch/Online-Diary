<script setup lang="ts">
import { Drawer, DrawerContent } from "@/components/ui/drawer";
import { Input } from "@/components/ui/input";
import Label from "@/components/ui/label/Label.vue";
import { Separator } from "@/components/ui/separator";
import BlockText from "@/modules/Common/Components/BlockText.vue";
import BooleanBlockText from "@/modules/Common/Components/BooleanBlockText.vue";
import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";

// форма конкретной дисциплины. Версия для экранов XS, SM и MD.

const { isXS, isSM, isMD } = useMediaScreenTypeTracker();

const props = defineProps<{
  isOpen: boolean;
}>();
</script>

<template>
  <Drawer v-if="isXS() || isSM() || isMD()" :open="props.isOpen">
    <DrawerContent>
      <section :class="'flex flex-col justify-center items-center gap-2'">
        <div
          :class="'p-4 rounded-t-md text-responsive-secondary font-semibold'"
        >
          Информация дисциплины
        </div>
        <div :class="'p-2 flex flex-col gap-2'">
          <Input :model-value="'Дискретная математика'" />
          <Input :model-value="'Семестр - 1'" />
          <Input :model-value="'Группа - 1'" />
          <Input :model-value="'Преподается - Федоров М.М.'" />
          <BooleanBlockText
            :value="false"
            :onTrue="'Архивна'"
            :onFalse="'Не архивна'"
          />
          <Separator :orientation="'horizontal'" />
          <div :class="'grid grid-cols-2 gap-2'">
            <Label :class="'text-responsive-tertiary'">Дата создания</Label>
            <BlockText :value="'12.12.2023'" />
          </div>
          <div :class="'grid grid-cols-2 gap-2'">
            <Label :class="'text-responsive-tertiary'">Дата изменения</Label>
            <BlockText :value="'12.12.2023'" />
          </div>
        </div>
      </section>
    </DrawerContent>
  </Drawer>
</template>
