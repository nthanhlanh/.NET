import { createStore } from 'vuex';
import axios from 'axios';
import router from '../router';

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
        const response = await axios.post('http://localhost:5246/api/auth/login', credentials);
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
        try {
          const userResponse = await axios.get('http://localhost:5246/api/User/details', {
            headers: { Authorization: `Bearer ${state.authToken}` }
          });
          if (userResponse.status === 200) {
            commit('setUser', userResponse.data);
          } 
        } catch (error) {
          console.error('Fetching user details failed:', error);
          commit('clearAuthToken');
          commit('clearUser');
          router.push('/login'); // Điều hướng về trang đăng nhập
        }
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
