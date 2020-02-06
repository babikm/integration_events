import Vue from 'vue';
import Vuex from 'vuex';
import { loginUrl } from '@/variables';

import router from '@/router';

Vue.use(Vuex);

const types = {
  LOGIN: 'LOGIN',
  LOGOUT: 'LOGOUT',
}

const state = {
  logged: localStorage.getItem('token'),
  currentUser: JSON.parse(localStorage.getItem('user')),
}


const getters = {
  isLogged: state => state.logged,
  getCurrentUser: state => state.currentUser
}

const actions = {
  login({ commit }, credential) {
    Vue.http.post(loginUrl, credential)
      .then(function(response) { 
       return response.json()
      })
      .then(function(result){
        const person = result;
        const jsonPerson = JSON.stringify(person);
        localStorage.setItem('user', jsonPerson);
        localStorage.setItem('token', result.tokenString);
        commit(types.LOGIN);
        router.push({ path: '/' });
        router.go(0);
      })
      .catch(err => {
        alert(err.body.message);
      });
  },

  logout({ commit }) {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
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