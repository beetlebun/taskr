/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'
import Vue3Toastify, { type ToastContainerOptions } from 'vue3-toastify'
// import axios from "axios";

const app = createApp(App)

app.use(Vue3Toastify, {
    autoClose: 3000,
    theme: 'dark'
  } as ToastContainerOptions)

registerPlugins(app)

app.mount('#app')
