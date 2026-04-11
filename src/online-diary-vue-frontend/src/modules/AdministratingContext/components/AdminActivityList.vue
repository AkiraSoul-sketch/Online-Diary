<script setup lang="ts">
import { Card, CardDescription, CardHeader } from "@/components/ui/card";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import { SearchIcon } from "lucide-vue-next";
import { Table, TableBody, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { onMounted, ref, type Ref } from "vue";
import type { AdminActivityItem } from "../admin.activity.models";
import { useAdminActivityStore } from "../admin.activity.store";

const activities: Ref<AdminActivityItem[]> = ref([]);
const store = useAdminActivityStore();

onMounted(() => {
  store.init(30);
  activities.value = store.items.sort((a, b) => b.date.getTime() - a.date.getTime());
})

</script>

<template>
  <Card :class="'card-primary rounded-md'">
    <CardHeader>
      <CardTitle :class="'text-responsive-primary'">
        Недавние действия
      </CardTitle>
      <CardDescription>
        <InputWithIcon :icon="SearchIcon" :place-holder="'фильтрация поиском'" />
      </CardDescription>
      <Table :no-wrapper="true">
        <TableHeader>
          <TableHead :class="'w-30'">
            <th :class="'text-responsive-secondary'">
              Инициатор
            </th>
          </TableHead>
          <TableHead :class="'w-15'">
            <th :class="'text-responsive-secondary'">
              Статус
            </th>
          </TableHead>
          <TableHead :class="'w-15'">
            <th :class="'text-responsive-secondary'">
              Дата
            </th>
          </TableHead>
          <TableHead>
            <th :class="'text-responsive-secondary'">
              Описание
            </th>
          </TableHead>
        </TableHeader>
        <TableBody :class="'overflow-auto'">
          <TableRow v-for="activity in activities" :key="activity.id">
            <TableHead :class="'text-responsive-tertiary'">
              {{ activity.actor }}
            </TableHead>
            <TableHead :class="'text-responsive-tertiary'">
              {{ activity.status }}
            </TableHead>
            <TableHead :class="'text-responsive-tertiary'">
              {{ activity.date.toLocaleString("ru-RU", { dateStyle: "short" }) }}
            </TableHead>
            <TableHead :class="'text-responsive-tertiary'">
              <span :class="'text-wrap'">
                {{ activity.action + activity.action }}
              </span>
            </TableHead>
          </TableRow>
        </TableBody>
      </Table>
    </CardHeader>
  </Card>
</template>