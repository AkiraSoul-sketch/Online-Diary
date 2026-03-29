import { defineStore } from "pinia";
import type {
  AdminPanelMenuCategory,
  AdminPanelMenuCategoryItem,
} from "./models/admin-panel.models";
import { ref, type Ref } from "vue";

export const useAdminStore = defineStore("admin", () => {
  const categories: Ref<AdminPanelMenuCategory[]> = ref([]);
  addCategory(categories, "Система", "left", [
    {
      menuName: "Аудит",
      route: "/admin/audit",
    },
  ]);
  addCategory(categories, "Пользователи", "item", [
    {
      menuName: "Пользователи",
      route: "/admin/users",
    },
  ]);
  addCategory(categories, "Учебные объекты", "right", [
    {
      menuName: "Дисциплины",
      route: "/admin/disciplines",
    },
  ]);

  return { categories };
});

function addCategory(
  categories: Ref<AdminPanelMenuCategory[]>,
  name: string,
  type: "left" | "right" | "item",
  items: AdminPanelMenuCategoryItem[] = [],
): void {
  const category: AdminPanelMenuCategory = {
    menuName: name,
    type,
    items,
  };
  categories.value.push(category);
}
