<script setup lang="ts">
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import { SearchIcon } from "lucide-vue-next";
import { onMounted, ref, type Ref } from "vue";
import type { AdminActivityItem } from "../admin.activity.models";
import { useAdminActivityStore } from "../admin.activity.store";
import { useWindowSize } from "@vueuse/core";

const activities: Ref<AdminActivityItem[]> = ref([]);
const store = useAdminActivityStore();
const windowSize = useWindowSize();

onMounted(() => {
  store.init(30);
  activities.value = store.items.sort((a, b) => b.date.getTime() - a.date.getTime());
})
</script>

<template>
  <Card :class="'card-primary activity-list-container rounded-md'">
    <CardHeader :class="'activity-list-head'">
      <CardTitle :class="'text-responsive-primary'">
        Недавние действия
      </CardTitle>
      <CardDescription>
        <InputWithIcon :icon="SearchIcon" :place-holder="'фильтрация поиском'" />
      </CardDescription>
    </CardHeader>
    <CardContent :class="'activity-list-content'">
      <div :class="'activity-list-scroll'">
        <table :class="'activity-list-table'">
          <thead :class="'item-bg-primary-accent activity-list-table-header'">
            <tr>
              <template v-if="windowSize.width.value > 400 && windowSize.width.value < 540">
                <th :class="'activity-list-table-cell'">
                  <p>статус</p>
                  <p>дата</p>
                  <p>инициатор</p>
                </th>
                <th :class="'activity-list-table-cell'">
                  <p>действие</p>
                </th>
              </template>
              <template v-if="windowSize.width.value > 540 && windowSize.width.value < 800">
                <th :class="'activity-list-table-cell activity-list-table-actor'">
                  <p>статус</p>
                  <p>инициатор</p>
                </th>
                <th :class="'activity-list-table-cell activity-list-table-actor'">
                  <p>дата</p>
                  <p>действие</p>
                </th>
              </template>
              <template v-if="windowSize.width.value > 800">
                <th :class="'activity-list-table-cell activity-list-table-actor'">инициатор</th>
                <th :class="'activity-list-table-cell activity-list-table-status'">статус</th>
                <th :class="'activity-list-table-cell activity-list-table-date'">дата</th>
                <th :class="'activity-list-table-cell activity-list-table-action'">действие</th>
              </template>
            </tr>
          </thead>

          <tbody>
            <tr :class="'item-bg-primary-accent-2'" v-for="item in activities" :key="item.id">
              <template v-if="windowSize.width.value > 400 && windowSize.width.value < 540">
                <td :class="'table-item'">
                  <p>{{ item.status }}</p>
                  <p>{{ item.date.toLocaleDateString('ru-Ru') }}</p>
                  <p>{{ item.actor }}</p>
                </td>
                <td :class="'table-item table-item-text-wrap'">{{ item.action }}</td>
              </template>
              <template v-if="windowSize.width.value > 540 && windowSize.width.value < 800">
                <td :class="'table-item'">
                  <p>{{ item.status }}</p>
                  <p>{{ item.actor }}</p>
                </td>
                <td :class="'table-item'">
                  <p>{{ item.date.toLocaleDateString('ru-Ru') }}</p>
                  <p :class="'table-item-text-wrap'">{{ item.action }}</p>
                </td>
              </template>
              <template v-if="windowSize.width.value > 800">
                <td :class="'table-item'">{{ item.actor }}</td>
                <td :class="'table-item'">{{ item.status }}</td>
                <td :class="'table-item'">{{ item.date.toLocaleDateString('ru-Ru') }}</td>
                <td :class="'table-item table-item-text-wrap'">{{ item.action }}</td>
              </template>
            </tr>
          </tbody>
        </table>
      </div>
      <div :class="'item-bg-primary-accent activity-list-table-foot'">
        <p :class="'text-center text-sm text-muted-foreground'">Показано 50 из 1000 записей</p>
      </div>
    </CardContent>
  </Card>
</template>

<style scoped lang="css">
.activity-list-container {
  min-height: 0;
  min-width: 0;
  display: grid;
  grid-template-rows: auto 1fr;
}

.activity-list-head {
  min-height: 0;
  min-width: 0;
  display: grid;
  grid-template-rows: auto auto;
  gap: 1em;
}

.activity-list-content {
  min-height: 0;
  min-width: 0;
  display: flex;
  flex-direction: column;
}

.activity-list-scroll {
  min-height: 0;
  flex: 1;
  overflow: auto;
}

.activity-list-table {
  width: 100%;
}

.activity-list-table tbody tr:nth-child(odd) {
  background-color: var(--bg-primary-accent-2);
}

.activity-list-table tbody tr:nth-child(even) {
  background-color: var(--bg-primary-accent);
}

.activity-list-table-header {
  height: 5%;
  position: sticky;
  top: 0;
  z-index: 1;
}

.activity-list-table-status {
  width: 7rem;
}

.activity-list-table-date {
  width: 7rem;
}

.activity-list-table-actor {
  width: 10rem;
}

.activity-list-table-action {
  width: auto;
}

.activity-list-table-cell {
  padding: 0.5em;
  text-align: left;
}

.table-item {
  padding: 0.75em;
}

.table-item-text-wrap {
  text-wrap: wrap;
}

.activity-list-table-foot {
  height: 5%;
}
</style>