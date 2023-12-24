import {
  mdiMonitor,
  mdiAccountGroupOutline,
  mdiBookOpen,
  mdiPageLayoutHeader,
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
    to: '/users',
    label: 'Bạn đọc',
    icon: mdiAccountGroupOutline
  },
  {
    to: '/portal',
    label: 'Cổng bạn đọc',
    icon: mdiPageLayoutHeader
  },


]
