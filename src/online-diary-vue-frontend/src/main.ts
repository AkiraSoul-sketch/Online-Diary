import { createApp } from "vue";
import App from "./App.vue";

import "@/styles/global.css";
import appRouter from "./routes";
import { createPinia } from "pinia";
import { createToastflow, ToastContainer } from "vue-toastflow";

const app = createApp(App);
const pinia = createPinia();
const toastFlow = createToastflow({ position: "bottom-right", duration: 5000 });
app.use(appRouter);
app.use(pinia);
app.use(toastFlow);

app.component("ToastContainer", ToastContainer);

app.mount("#app");
