import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '../views/DashboardView.vue'
import ProductsView from '../views/ProductsView.vue'
import ChannelsView from '../views/ChannelsView.vue'
import SyncJobsView from '../views/SyncJobsView.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', name: 'dashboard', component: DashboardView },
    { path: '/products', name: 'products', component: ProductsView },
    { path: '/channels', name: 'channels', component: ChannelsView },
    { path: '/sync-jobs', name: 'sync-jobs', component: SyncJobsView }
  ]
})

export default router
