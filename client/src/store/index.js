import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    currentUser: {
      name: "Janek",
      role: "Admin",
      email: "janek@janeko2.pl"
    },
  },
  getters: {
    getCurrentUser: state => state.currentUser
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})
