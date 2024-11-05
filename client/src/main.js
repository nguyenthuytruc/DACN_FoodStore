import "./assets/main.css";

import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import axios from "axios";

// Cấu hình axios mặc định
axios.defaults.baseURL = "https://your-api-url.com";
axios.defaults.headers.common["Authorization"] = "Bearer token";
axios.defaults.headers.post["Content-Type"] = "application/json";

const app = createApp(App);

app.config.globalProperties.$axios = axios;

app.use(router);

app.mount("#app");
