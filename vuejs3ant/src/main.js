import { createApp } from 'vue'
import App from './App.vue'
import Antd from 'ant-design-vue';
// import 'ant-design-vue/dist/antd.css'; // Import CSS của Ant Design Vue

const app = createApp(App);

app.use(Antd); // Sử dụng Ant Design Vue
app.mount('#app');
