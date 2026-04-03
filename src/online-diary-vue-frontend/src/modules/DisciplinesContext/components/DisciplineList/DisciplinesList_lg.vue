<script setup lang="ts">
// Список дисциплин. Версия для экранов LG.

import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";
import { useDisciplinesStore } from "../../discipline.store";
import InlineText from "@/modules/Common/Components/InlineText.vue";
import BlockText from "@/modules/Common/Components/BlockText.vue";
import BooleanBlockText from "@/modules/Common/Components/BooleanBlockText.vue";

const { isLG } = useMediaScreenTypeTracker();
const store = useDisciplinesStore();
const tableCellStyle: string = "p-3 text-left whitespace-nowrap";
const cellStyle = "grid grid-cols-2 gap-2";
</script>

<template>
  <div v-if="isLG()" class="rounded-lg overflow-auto">
    <table class="w-full flex-1 min-h-0 min-w-0">
      <tbody class="divide-y divide-gray-100">
        <!-- записи дисциплин -->
        <tr v-for="discipline of store.disciplines">
          <td :class="tableCellStyle">
            <InlineText :value="discipline.name" :placeholder="'-'" />
          </td>
          <td :class="tableCellStyle">
            <div :class="cellStyle">
              <BlockText
                :class="'font-semibold'"
                :value="'Семестр'"
                :placeholder="'-'"
              />
              <BlockText
                :value="discipline.semester?.number"
                :placeholder="'-'"
              />
            </div>
            <div :class="cellStyle">
              <BlockText
                :class="'font-semibold'"
                :value="'Группа'"
                :placeholder="'-'"
              />
              <BlockText :value="discipline.group?.name" :placeholder="'-'" />
            </div>
            <div :class="cellStyle">
              <BlockText
                :class="'font-semibold'"
                :value="'Преподается'"
                :placeholder="'-'"
              />
              <BlockText :value="discipline.teacher?.name" :placeholder="'-'" />
            </div>
          </td>
          <td :class="tableCellStyle">
            <div :class="cellStyle">
              <BlockText
                :class="'font-semibold'"
                :value="'Дата создания'"
                :placeholder="'-'"
              />
              <BlockText
                :value="discipline.lifeTime.createdAt"
                :placeholder="'-'"
              />
            </div>
            <div :class="'grid grid-cols-2 gap-2'">
              <BlockText
                :class="'font-semibold'"
                :value="'Дата изменения'"
                :placeholder="'-'"
              />
              <BlockText
                :value="discipline.lifeTime.updatedAt"
                :placeholder="'-'"
              />
            </div>
            <div :class="'grid grid-cols-2 gap-2'">
              <BlockText
                :class="'font-semibold'"
                :value="'Архив'"
                :placeholder="'-'"
              />
              <BooleanBlockText
                :value="discipline.lifeTime.archived"
                :onTrue="'да'"
                :onFalse="'-'"
              />
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
