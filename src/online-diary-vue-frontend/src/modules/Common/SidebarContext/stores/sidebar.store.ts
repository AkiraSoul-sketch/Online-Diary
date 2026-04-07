import { defineStore } from "pinia";
import type { SidebarNavigation } from "../models/sidebar.models";
import { ref, type Ref } from "vue";

export const useSidebarStore = defineStore("sidebar", () => {
  const navigations = ref<SidebarNavigation[]>([]);
  appendNavigation("Журналы", "/grades", navigations);
  appendNavigation("Администрирование", "/admin/audit", navigations);
  appendNavigation("Дисциплины", "/disciplines", navigations);
  appendNavigation("Преподаватель", "/teacher", navigations);
  return { navigations };
});

function createNavigation(name: string, link: string): SidebarNavigation {
  const navigation: SidebarNavigation = { name, link };
  return navigation;
}

function appendNavigation(
  name: string,
  link: string,
  navigationRef: Ref<
    {
      name: string;
      link: string;
    }[],
    | SidebarNavigation[]
    | {
        name: string;
        link: string;
      }[]
  >,
): void {
  const navigation = createNavigation(name, link);
  navigationRef.value.push(navigation);
}
