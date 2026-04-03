<script setup lang="ts">
// список дисциплин. Версия для экранов XL и XXL.

import { useMediaScreenTypeTracker } from "@/modules/Common/Composables/useMediaScreenTypeTracker";
import { useDisciplinesStore } from "../../discipline.store";
import InlineText from "@/modules/Common/Components/InlineText.vue";
import BooleanBlockText from "@/modules/Common/Components/BooleanBlockText.vue";
import { classConstructor } from "@/modules/Common/ComponentsLogic/classConstructor";

const { isXL, isXXL } = useMediaScreenTypeTracker();
const store = useDisciplinesStore();
const headerStyle: string =
  "p-3 text-sm font-semibold tracking-wide text-left whitespace-nowrap";
</script>

<template>
  <div v-if="isXL() || isXXL()" :class="'rounded-lg'">
    <table :class="'w-full'">
      <!-- шапка таблицы -->
      <thead :class="'bg-gray-50 border-b-2 border-gray-200'">
        <tr>
          <th :class="classConstructor(headerStyle)">Название</th>
          <th :class="classConstructor(headerStyle, 'w-10')">Семестр</th>
          <th :class="classConstructor(headerStyle, 'w-16')">Группа</th>
          <th :class="classConstructor(headerStyle, 'w-32')">Преподается</th>
          <th :class="classConstructor(headerStyle, 'w-24')">Дата создания</th>
          <th :class="classConstructor(headerStyle, 'w-24')">Дата изменения</th>
          <th :class="classConstructor(headerStyle, 'w-10')">Архив</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-gray-100">
        <!-- записи дисциплин -->
        <tr v-for="discipline of store.disciplines" :key="discipline.id">
          <td class="p-3 text-left whitespace-nowrap">
            <InlineText :value="discipline.name" :placeholder="''" />
          </td>
          <td class="p-3 text-left whitespace-nowrap">
            <InlineText
              :value="discipline.semester?.number"
              :placeholder="'-'"
            />
          </td>
          <td class="p-3 text-left whitespace-nowrap">
            <InlineText :value="discipline.group?.name" :placeholder="'-'" />
          </td>
          <td class="p-3 text-left whitespace-nowrap">
            <InlineText :value="discipline.teacher?.name" :placeholder="'-'" />
          </td>
          <td class="p-3 text-left whitespace-nowrap">
            <InlineText
              :value="discipline.lifeTime.createdAt.toLocaleDateString()"
              :placeholder="'-'"
            />
          </td>
          <td class="p-3 text-left whitespace-nowrap">
            <InlineText
              :value="discipline.lifeTime.updatedAt?.toLocaleDateString()"
              :placeholder="'-'"
            />
          </td>
          <td class="p-3 text-left whitespace-nowrap">
            <BooleanBlockText
              :value="discipline.lifeTime.archived"
              :onTrue="'да'"
              :onFalse="'-'"
            />
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
