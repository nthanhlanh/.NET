import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import 'mdb-vue-ui-kit/css/mdb.min.css';
import '@fortawesome/fontawesome-free/css/all.min.css';

const app = createApp(App);

app.use(router); // Nếu bạn sử dụng vue-router
app.use(store); // Nếu bạn sử dụng vuex

// Khi ứng dụng khởi động, kiểm tra token và lấy lại thông tin người dùng nếu có
store.dispatch('fetchUser');

app.mount('#app');
