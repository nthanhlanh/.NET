import { createStore } from 'vuex';
import axios from 'axios';
import userApi from '@/api/userApi';

export default createStore({
  state: {
    authToken: localStorage.getItem('authToken') || null,
    user: null,
    isLoggedIn: localStorage.getItem('authToken') != null ? true : false,
  },
  mutations: {
    setAuthToken(state, token) {
      state.authToken = token;
      state.isLoggedIn = true;
      localStorage.setItem('authToken', token);
    },
    clearAuthToken(state) {
      state.authToken = null;
      state.isLoggedIn = false;
      localStorage.removeItem('authToken');
    },
    setUser(state, user) {
      state.user = user;
    },
    clearUser(state) {
      state.user = null;
    }
  },
  actions: {
    async login({ commit, dispatch }, credentials) {
      try {
        var defaultHostUrl = process.env.VUE_APP_API_BASE_URL || 'http://localhost:5246';
        const response = await axios.post(`${defaultHostUrl}/api/auth/login`, credentials);
        if (response.status === 200) {
          const token = response.data.token; // Thay đổi dựa trên cấu trúc dữ liệu của bạn
          commit('setAuthToken', token);

          // Gọi fetchUser sau khi đăng nhập thành công
          await dispatch('fetchUser');
        }
      } catch (error) {
        console.error('Login failed:', error);
        throw error; // Để thông báo lỗi có thể được xử lý bên ngoài
      }
    },
    async fetchUser({ commit, state }) {
      if (state.authToken) {
        const userResponse = await userApi.getUserDetails();
        commit('setUser', userResponse.data);
      }
    },
    logout({ commit }) {
      commit('clearAuthToken');
      commit('clearUser');
    }
  },
  getters: {
    isLoggedIn(state) {
      return state.isLoggedIn;
    },
    user(state) {
      return state.user;
    }
  }
});
