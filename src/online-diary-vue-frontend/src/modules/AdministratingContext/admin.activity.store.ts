import { defineStore } from "pinia";
import type { AdminActivityItem } from "./admin.activity.models";
import { ref, type Ref } from "vue";

function randomDateWithinDays(days = 30): Date {
  const now = Date.now();
  const past = now - Math.floor(Math.random() * days * 24 * 60 * 60 * 1000);
  return new Date(past - Math.floor(Math.random() * 24 * 60 * 60 * 1000));
}

function pick<T>(arr: T[]): T {
  return arr[Math.floor(Math.random() * arr.length)];
}

function generateItems(count = 50): Array<AdminActivityItem> {
  const actors = [
    "ivan.petrov",
    "anna.sidorova",
    "oleg.kuznetsov",
    "maria.ivanova",
    "system",
    "admin",
  ];
  const actions = [
    "создал запись",
    "отредактировал запись",
    "удалил запись",
    "вошёл в систему",
    "вышел из системы",
    "изменил роль",
    "обновил планы",
  ];
  const statuses = ["success", "warning", "failed", "info"];

  const items: AdminActivityItem[] = [];
  for (let i = 1; i <= count; i++) {
    items.push({
      id: i,
      date: randomDateWithinDays(30),
      actor: pick(actors),
      action: pick(actions),
      status: pick(statuses),
    });
  }
  return items;
}

export const useAdminActivityStore = defineStore("adminActivity", () => {
  const items: Ref<Array<AdminActivityItem>> = ref([]);

  function init(count = 50) {
    items.value = generateItems(count);
  }

  function add(item: AdminActivityItem) {
    items.value.unshift(item);
  }

  function clear() {
    items.value = [];
  }

  init();

  return {
    items,
    init,
    add,
    clear,
  };
});
