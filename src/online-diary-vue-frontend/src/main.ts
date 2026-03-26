import { createApp } from "vue";
import App from "./App.vue";
import "./styles/global.css";
import appRouter from "./routes";
import { createPinia } from "pinia";

const app = createApp(App);
const pinia = createPinia();
app.use(appRouter);
app.use(pinia);
app.mount("#app");
