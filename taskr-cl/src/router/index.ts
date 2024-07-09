/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from "vue-router/auto";
import home from "@/pages/home.vue";
import login from "@/pages/login.vue";
import { is_logged_in } from "@/services/auth_service";

const routes = [
  {
    path: "/",
    name: "home",
    component: home,
  },
  {
    path: "/login",
    name: "login",
    component: login,
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

router.beforeEach((to: any, from: any, next: any) => {
  if (to.name !== "/login" && !is_logged_in()) {
    next({ path: "/login", replace: true });
  } else if (
    (to.name === "/login" || to.name === "/" || to.name === undefined) &&
    is_logged_in()
  ) {
    next({ path: "/home", replace: true });
  } else {
    next();
  }
});

export default router;
