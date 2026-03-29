import type { RouteRecordRaw } from "vue-router";

export const adminRoutes: RouteRecordRaw[] = [
  {
    path: "users",
    component: () =>
      import("@/modules/UsersManagementContext/UsersManagementPage.vue"),
  },
  {
    path: "disciplines",
    component: () => import("@/modules/DisciplinesContext/DisciplinesPage.vue"),
  },
  {
    path: "audit",
    component: () =>
      import("@/modules/AdministratingContext/component/AdminPanelMenu/AdminAuditPage.vue"),
  },
];
