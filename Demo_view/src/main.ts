import 'assets/main.css';
import 'quasar/dist/quasar.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import { VueSignalR }  from '@dreamonkey/vue-signalr';
import { HubConnectionBuilder } from '@microsoft/signalr';

import App from './App.vue'
import router from './router'
import CIcon from 'base/CIcon.vue';
import { Quasar} from 'quasar'
import quasarUserOptions from './quasar-user-options'
const app = createApp(App).use(Quasar, quasarUserOptions)
// app.config.errorHandler = () => null;
app.config.warnHandler = () => null;

// config signalR
const connection = new HubConnectionBuilder()
  .withUrl('https://localhost:7082/comment')
  .build();
app.use(VueSignalR, { connection } as any)

app.use(createPinia())
app.use(router)

// Register global component
//TODO: get rid of any
app.component('CIcon', CIcon as any);

app.mount('#app')
