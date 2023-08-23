import "./styles/global.scss";
import router from './router/router.js';
import FontAwesomeIcon from './infrastructure/iconImporter.js';
import { createApp } from 'vue';
import App from './App.vue';

let app = createApp(App);
app.use(router);
app.component('font-awesome-icon', FontAwesomeIcon);
app.mount('#app');