import axios from 'axios';
import store from '@/store'; // Import Vuex store
import router from '../router';

// Create an axios instance
const apiClient = axios.create({
  baseURL: process.env.VUE_APP_API_BASE_URL || 'http://localhost:5246/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add a request interceptor to attach the token to every request
apiClient.interceptors.request.use(
  config => {
    const token = store.state.authToken;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

// Handle responses globally
apiClient.interceptors.response.use(
  response => response,
  error => {
    if (error.response && error.response.status === 401) {
      store.commit('clearAuthToken');
      store.commit('clearUser');
      router.push('/login');
    }
    return Promise.reject(error);
  }
);

export default apiClient;
