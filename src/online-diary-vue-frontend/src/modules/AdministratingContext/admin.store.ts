import { defineStore } from "pinia";
import type {
  AdminPanelMenuCategory,
  AdminPanelMenuCategoryItem,
} from "./components/AdminPanelMenu/models/admin-panel.models";
import { computed, ref, type Ref } from "vue";

export const useAdminStore = defineStore("admin", () => {
  const menuTitleValue: Ref<string> = ref("Аудит");
  const categories: Ref<AdminPanelMenuCategory[]> = ref([]);
  const menuTitle = computed(() => menuTitleValue.value);

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

  function changeTitle(newTitle: string): void {
    menuTitleValue.value = newTitle;
  }

  return { categories, changeTitle, menuTitle };
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
