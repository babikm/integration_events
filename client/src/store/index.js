import Vue from 'vue';
import Vuex from 'vuex';
import { loginUrl } from '@/variables';

Vue.use(Vuex);

const types = {
  LOGIN: 'LOGIN',
  LOGOUT: 'LOGOUT',
}

const state = {
  logged: localStorage.getItem('token'),
  currentUser: {
    name: "Janek",
    role: "Admin",
    email: "janek@janeko2.pl"
  },
}


const getters = {
  isLogged: state => state.logged,
  getCurrentUser: state => state.currentUser
}

const actions = {
  login({ commit }, credential) {
    Vue.http.post(loginUrl, credential)
      .then(response => response.json())
      .then((result) => {
        localStorage.setItem('token', result.accessToken);
        commit(types.LOGIN);
        router.push({ path: '/' });
      })
      .catch(err => console.log(err));
  },

  logout({ commit }) {
    localStorage.removeItem('token');
    commit(types.LOGOUT);
    router.push({ path: '/login' });
  },
}

const mutations = {
  [types.LOGIN] (state) {
    state.logged = 1;
  },

  [types.LOGOUT] (state) {
    state.logged = 0;
  },
}

export default new Vuex.Store({
  state,
  getters,
  actions,
  mutations
});