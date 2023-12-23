import { defineStore } from 'pinia'
import { ref } from 'vue'
import httpclient from '@/apis/httpclient'
import { useContextStore } from '@/stores/contextStore.js'
import { useRouter } from 'vue-router'

export const useAuthStore = defineStore('login', () => {
  const context = useContextStore()
  const router = useRouter()

  const controlName = ref('Auth')

  const items = ref([])

  const loading = ref(false)

  const buildUrl = () => {
    return window._apis['Business'] + '/' + controlName.value
  }

  const login = async (payload) => {
    loading.value = true
    await httpclient
      .requestAsync(
        {
          url: buildUrl() + '/Login',
          headers: {},
          data: payload
        },
        'POST'
      )
      .then((res) => {
        context.user = res.data
        context.Token = res.data?.token
        localStorage.setItem('context', JSON.stringify(res.data))
        router.push({
          name: 'dashboard'
        })
        setTimeout(() => {
          location.reload()
        }, 500)
      })
      .catch((res) => {
        return res.data
      })
      .finally(() => {
        loading.value = false
      })
  }

  return {
    items,
    login
  }
})
