import { mdiAccount, mdiCogOutline, mdiEmail, mdiLogout, mdiThemeLightDark } from '@mdi/js'

export default [
  {
    icon: mdiThemeLightDark,
    label: 'Light/Dark',
    isDesktopNoLabel: true,
    isToggleLightDark: true
  },
  {
    isCurrentUser: true,
    menu: [
      {
        icon: mdiAccount,
        label: 'Hồ sơ của tôi',
        to: '/profile'
      },
      {
        icon: mdiLogout,
        label: 'Đăng xuất',
        isLogout: true,
        to: '/login'
      }
    ]
  }
]
