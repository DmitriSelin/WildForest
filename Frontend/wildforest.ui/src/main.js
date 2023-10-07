/* eslint-disable vue/multi-word-component-names */
import "./styles/global.scss";
import router from './router/router.js';
import FontAwesomeIcon from './infrastructure/iconImporter.js';
import { createApp } from 'vue';
import App from './App.vue';
import { createPinia } from 'pinia'

/* PrimeVue */
import PrimeVue from 'primevue/config';
import 'primevue/resources/primevue.min.css';
import 'primevue/resources/themes/bootstrap4-dark-blue/theme.css';
import Dropdown from 'primevue/dropdown';

/* Pinia */
const pinia = createPinia();

/* App configuration */
let app = createApp(App);
app.use(router);
app.use(PrimeVue);
app.use(pinia);
app.component('font-awesome-icon', FontAwesomeIcon);

/* PrimeVue components */
app.component('Dropdown', Dropdown);
/* PrimeVue components */

app.mount('#app');