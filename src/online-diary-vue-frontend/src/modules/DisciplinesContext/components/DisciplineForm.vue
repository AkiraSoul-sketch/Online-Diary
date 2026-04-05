<script setup lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import CardHeader from "@/components/ui/card/CardHeader.vue";
import Input from "@/components/ui/input/Input.vue";
import Label from "@/components/ui/label/Label.vue";
import Separator from "@/components/ui/separator/Separator.vue";
import BlockText from "@/modules/Common/Components/BlockText.vue";
import BooleanBlockText from "@/modules/Common/Components/BooleanBlockText.vue";
import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";

// форма для конкретной дисциплины. Версия для экранов LG, XL и XXL.

const { isLG, isXL, isXXL } = useMediaScreenTypeTracker();
</script>

<template>
  <Card v-if="isLG() || isXL() || isXXL()" :class="'p-0 gap-4 card-primary'">
    <CardHeader :class="'p-4 bg-jade-green rounded-t-md text-responsive-secondary font-semibold'">
      Информация дисциплины
    </CardHeader>
    <CardContent :class="'p-2 flex flex-col gap-2'">
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
    </CardContent>
  </Card>
</template>
