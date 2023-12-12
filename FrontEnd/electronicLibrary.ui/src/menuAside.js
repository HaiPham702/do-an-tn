import {
  mdiAccountCircle,
  mdiMonitor,
  mdiLock,
  mdiAlertCircle,
  mdiAccountGroupOutline,
  mdiBookOpen,
  mdiViewList,
  mdiCog,
  mdiResponsive,
  mdiPalette
} from '@mdi/js'

export default [
  {
    to: '/',
    icon: mdiMonitor,
    label: 'Tổng quan'
  },
  {
    to: '/books',
    label: 'Sách',
    icon: mdiBookOpen
  },
  {
    to: '/forms',
    label: 'Bạn đọc',
    icon: mdiAccountGroupOutline
  },
  {
    to: '/ui',
    label: 'Hệ thống',
    icon: mdiCog
  },


]
