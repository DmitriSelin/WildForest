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
import Toast from 'primevue/toast';
import ToastService from 'primevue/toastservice';
import Message from 'primevue/message';
import FileUpload from 'primevue/fileupload';
import Avatar from 'primevue/avatar';

/* Pinia */
const pinia = createPinia();

/* App configuration */
let app = createApp(App);
app.use(router);
app.use(PrimeVue);
app.use(ToastService);
app.use(pinia);
app.component('font-awesome-icon', FontAwesomeIcon);

/* PrimeVue components */
app.component('Dropdown', Dropdown);
app.component('Toast', Toast);
app.component('Message', Message);
app.component('FileUpload', FileUpload);
app.component('Avatar', Avatar);
/* PrimeVue components */

app.mount('#app');