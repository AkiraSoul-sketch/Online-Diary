import { createRouter, createWebHistory } from "vue-router";
import { adminRoutes } from "./modules/AdministratingContext/admin.routes";

const appRouter = createRouter({
  routes: [
    {
      path: "/",
      component: () => import("/src/Placeholder.vue"),
    },
    {
      path: "/admin",
      component: () => import("@/modules/AdministratingContext/AdminPanel.vue"),
      children: adminRoutes,
    },
    {
      path: "/grades",
      component: () => import("/src/modules/GradesContext/GradesPage.vue"),
    },
    {
      path: "/disciplines",
      component: () =>
        import("/src/modules/DisciplinesContext/DisciplinesPage.vue"),
    },
    {
      path: "/teacher",
      component: () => import("/src/modules/TeacherContext/OD-TeacherPage.vue"),
    },
  ],
  history: createWebHistory(),
});

export default appRouter;
