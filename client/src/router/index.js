import Vue from "vue";
import VueRouter from "vue-router";
import events from "@/components/Event/events.vue";
import addEvent from "@/components/Event/addEvent.vue";
import editEvent from "@/components/Event/editEvent.vue";
import singleEvent from "@/components/Event/singleEvent.vue";
import userEvents from "@/components/Event/userEvents.vue";
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
      path: "/addEvent",
      component: addEvent,
      meta: { requiresAuth: true }
    },
    {
      path: '/event/:id',
      component: singleEvent,
      meta: { requiresAuth: true }
    },
    {
      path: '/event/:id/edit',
      component: editEvent,
      meta: { requiresAuth: true }
    },
    {
      path: '/userEvents',
      component: userEvents,
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
    if (!localStorage.getItem('token')) {
      next("/login");
    } else next();
  } else {
    next();
  }
});

export default router;
