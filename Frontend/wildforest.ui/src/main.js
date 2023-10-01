import "./styles/global.scss";
import router from './router/router.js';
import FontAwesomeIcon from './infrastructure/iconImporter.js';
import { createApp } from 'vue';
import App from './App.vue';
import PrimeVue from 'primevue/config';
import 'primevue/resources/primevue.min.css';
import 'primevue/resources/themes/saga-blue/theme.css';

let app = createApp(App);
app.use(router);
app.use(PrimeVue);
app.component('font-awesome-icon', FontAwesomeIcon);
app.mount('#app');