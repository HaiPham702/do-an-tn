import { createRouter, createWebHistory } from 'vue-router'

export default createRouter({
  history: createWebHistory(),
  routes: [
    {
        path: '/',
        component: () => import('@/components/pdfReader/index.vue'),
      },
    {
      path: '/book-reader',
      component: () => import('@/components/pdfReader/index.vue'),
    },
  ],
})
