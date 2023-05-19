"use strict";

import { createRouter, createWebHistory } from "vue-router";
import Game from "./views/Game.vue";
import ManageQuestions from "./views/ManageQuestions.vue";

const routes = [
  {
    path: "/",
    name: "Game",
    component: Game,
  },
  {
    path: "/questions",
    name: "ManageQuestions",
    component: ManageQuestions,
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.VITE_BASE_URL),
  routes,
});

export default router;
