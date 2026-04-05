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
  <Drawer :fixed="true" v-if="isXS() || isSM() || isMD()" :open="props.isOpen">
    <DrawerContent :class="'card-primary'">
      <section :class="'flex flex-col justify-center items-center gap-2'">

        <div :class="'p-4 rounded-t-md text-responsive-secondary font-semibold'">
          Информация дисциплины
        </div>

        <div :class="'p-2 flex flex-col gap-2'">
          <Input :class="'text-responsive-tertiary'" :model-value="'Дискретная математика'" />
          <Input :class="'text-responsive-tertiary'" :model-value="'Семестр - 1'" />
          <Input :class="'text-responsive-tertiary'" :model-value="'Группа - 1'" />
          <Input :class="'text-responsive-tertiary'" :model-value="'Преподается - Федоров М.М.'" />
          <BooleanBlockText :class="'text-responsive-tertiary'" :value="false" :onTrue="'Архивна'"
            :onFalse="'Не архивна'" />

          <Separator :orientation="'horizontal'" />

          <div :class="'grid grid-cols-2 gap-2'">
            <Label :class="'text-responsive-tertiary'">Дата создания</Label>
            <BlockText :class="'text-responsive-tertiary'" :value="'12.12.2023'" />
          </div>

          <div :class="'grid grid-cols-2 gap-2'">
            <Label :class="'text-responsive-tertiary'">Дата изменения</Label>
            <BlockText :class="'text-responsive-tertiary'" :value="'12.12.2023'" />
          </div>
        </div>

      </section>
    </DrawerContent>
  </Drawer>
</template>
