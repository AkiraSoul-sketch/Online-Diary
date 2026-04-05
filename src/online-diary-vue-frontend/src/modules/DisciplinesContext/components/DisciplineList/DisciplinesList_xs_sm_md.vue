<script setup lang="ts">
// Список дисциплин. Версия для экранов XS, SM и MD.

import BlockText from "@/modules/Common/Components/BlockText.vue";
import BooleanBlockText from "@/modules/Common/Components/BooleanBlockText.vue";
import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";
import { useDisciplinesStore } from "@/modules/DisciplinesContext/discipline.store";

const store = useDisciplinesStore();
const { isMD, isSM, isXS } = useMediaScreenTypeTracker();
const cellClass: string = "grid grid-cols-[135px_1fr] gap-2";
</script>

<template>
  <div v-if="isMD() || isSM() || isXS()" :class="'grid grid-cols-1 sm:grid-cols-2 sm:w-full gap-2'">
    <!-- записи дисциплин -->
    <div v-for="discipline of store.disciplines" :key="discipline.id" :class="'card-primary p-4 rounded-lg'">
      <div :class="cellClass">
        <BlockText :class="'font-semibold'" :value="'Название'" :placeholder="''" />
        <BlockText :value="discipline.name" :placeholder="''" />
      </div>
      <div :class="cellClass">
        <BlockText :class="'font-semibold'" :value="'Группа'" :placeholder="''" />
        <BlockText :value="discipline.group?.name" :placeholder="'-'" />
      </div>
      <div :class="cellClass">
        <BlockText :class="'font-semibold'" :value="'Семестр'" :placeholder="''" />
        <BlockText :value="discipline.semester?.number" :placeholder="'-'" />
      </div>
      <div :class="cellClass">
        <BlockText :class="'font-semibold'" :value="'Преподается'" :placeholder="''" />
        <BlockText :value="discipline.teacher?.name" :placeholder="'-'" />
      </div>
      <div :class="cellClass">
        <BlockText :class="'font-semibold'" :value="'Дата создания'" :placeholder="''" />
        <BlockText :value="discipline.lifeTime.createdAt" :placeholder="'-'" />
      </div>
      <div :class="cellClass">
        <BlockText :class="'font-semibold'" :value="'Дата изменения'" :placeholder="''" />
        <BlockText :value="discipline.lifeTime.updatedAt" :placeholder="'-'" />
      </div>
      <div :class="cellClass">
        <BlockText :class="'font-semibold'" :value="'Архив'" :placeholder="''" />
        <BooleanBlockText :value="discipline.lifeTime.archived" :onTrue="'да'" :onFalse="'-'" />
      </div>
    </div>
  </div>
</template>
