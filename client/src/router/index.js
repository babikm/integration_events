import Vue from "vue";
import VueRouter from "vue-router";
import events from "@/components/Event/events.vue";
import addEvent from "@/components/Event/addEvent.vue";
import singleEvent from "@/components/Event/singleEvent.vue";
import login from "@/components/Auth/login.vue";
import register from "@/components/Auth/register.vue";
import notFound404 from "@/components/notFound404.vue";
import store from '@/store';

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  routes: [
    {
      path: "/",
      component: events,
      meta: { requiresAuth: true }
    },
    {
      path: "/add-event",
      component: addEvent,
      meta: { requiresAuth: true }
    },
    {
      path: '/event/:id',
      component: singleEvent,
      meta: { requiresAuth: true }
    },
    {
      path: "/login",
      component: login,
      meta: { requiresAuth: false }
    },
    {
      path: "/register",
      component: register,
      meta: { requiresAuth: false }
    },
    {
      path: "*",
      component: notFound404,
      meta: { requiresAuth: false }
    }
  ]
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.getCurrentUser.name !== "Janek") {
      next("/");
    } else next();
  } else {
    next();
  }
});

export default router;
