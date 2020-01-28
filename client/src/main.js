import Vue from 'vue'
import VueResource from "vue-resource";
import { library } from '@fortawesome/fontawesome-svg-core'
import {
  faTrashAlt,
  faEdit,
  faEraser,
  faPlus,
  faDoorOpen,
  faUser,
  faIdCard,
  faUserShield,
  faEnvelope,
  faAngleDoubleLeft,
  faCheck,
  faFileExport,
  faArrowLeft,
  faLock,
  faUserTie
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import App from './App.vue'
import router from './router'
import store from './store'

Vue.config.productionTip = false

Vue.use(VueResource);

library.add(
  faTrashAlt,
  faEdit,
  faEraser,
  faPlus,
  faDoorOpen,
  faUser,
  faIdCard,
  faUserShield,
  faEnvelope,
  faAngleDoubleLeft,
  faCheck,
  faFileExport,
  faArrowLeft,
  faLock,
  faUserTie
);

Vue.component("font-awesome-icon", FontAwesomeIcon);

new Vue({
  router,
  store,
  render: function (h) { return h(App) }
}).$mount('#app')
