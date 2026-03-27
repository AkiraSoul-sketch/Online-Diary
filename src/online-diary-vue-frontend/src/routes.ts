import { createRouter, createWebHistory } from "vue-router";

const appRouter = createRouter({
  routes: [
    {
      path: "/",
      component: () => import("/src/Placeholder.vue"),
    },
    {
      path: "/admin",
      component: () =>
        import("/src/modules/AdministratingContext/AdminPanel.vue"),
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
  ],
  history: createWebHistory(),
});

export default appRouter;
