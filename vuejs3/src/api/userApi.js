import apiClient from './axios';

export default {
  async fetchUsers(page, pageSize) {
    const response = await apiClient.get('/User', {
      params: { page, pageSize }
    });
    return response.data;
  },

  async getUserDetails() {
    const response = await apiClient.get('/User/details');
    return response.data;
  },

  async login(credentials) {
    const response = await apiClient.post('/auth/login', credentials);
    return response.data;
  },
};
