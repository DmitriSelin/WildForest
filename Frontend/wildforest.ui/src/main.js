import './styles/global.css';

import router from './router/router';

import { createApp } from 'vue';
import App from './App.vue';

let app = createApp(App);
app.use(router);
app.mount('#app');