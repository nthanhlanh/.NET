import { createStore } from 'vuex';
import axios from 'axios';

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
    removeAuthToken(state) {
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
    async login({ commit }, credentials) {
      try {
        const response = await axios.post('http://localhost:5246/api/auth/login', credentials);
        const token = response.data.token; // Thay đổi dựa trên cấu trúc dữ liệu của bạn
        commit('setAuthToken', token);

        // Lưu thông tin người dùng nếu có
        // const userResponse = await axios.get('http://localhost:5246/api/auth/user', {
        //   headers: { Authorization: `Bearer ${token}` }
        // });
        // commit('setUser', userResponse.data);

      } catch (error) {
        console.error('Login failed:', error);
        throw error; // Để thông báo lỗi có thể được xử lý bên ngoài
      }
    },
    logout({ commit }) {
      commit('removeAuthToken');
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
