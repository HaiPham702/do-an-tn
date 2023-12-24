import { createRouter, createWebHashHistory } from 'vue-router'

const routes = [
  {
    // Document title tag
    // We combine it with defaultDocumentTitle set in `src/main.js` on router.afterEach hook
    meta: {
      title: 'Tổng quan'
    },
    path: '/',
    name: 'dashboard',
    component: () => import('@/views/HomeView.vue')
  },
  {
    meta: {
      title: 'Sách'
    },
    path: '/books',
    name: 'books',
    component: () => import('@/views/Books.vue')
  },
  {
    meta: {
      title: 'Bạn đọc'
    },
    path: '/users',
    name: 'users',
    component: () => import('@/views/UserView.vue')
  },
  {
    meta: {
      title: 'Hồ sơ'
    },
    path: '/profile',
    name: 'profile',
    component: () => import('@/views/ProfileView.vue')
  },
  {
    meta: {
      title: 'Ui'
    },
    path: '/ui',
    name: 'ui',
    component: () => import('@/views/UiView.vue')
  },
  {
    meta: {
      title: 'Đăng nhập'
    },
    path: '/login',
    name: 'login',
    component: () => import('@/views/LoginView.vue')
  },
  {
    meta: {
      title: 'Error'
    },
    path: '/error',
    name: 'error',
    component: () => import('@/views/ErrorView.vue')
  },

  {
    meta: {
      title: 'Cổng bạn đọc'
    },
    path: '/portal',
    name: 'portal',
    component: () => import('@/views/Portal/Index.vue')
  },
  {
    meta: {
      title: 'Cổng bạn đọc'
    },
    path: '/book-view',
    name: 'bookView',
    component: () => import('@/views/Portal/ViewBook.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    return savedPosition || { top: 0 }
  }
})

export default router
