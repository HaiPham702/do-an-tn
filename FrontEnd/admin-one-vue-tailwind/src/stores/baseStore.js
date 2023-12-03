import { defineStore } from 'pinia'
import { ref, watch } from 'vue'
import { ElLoading } from 'element-plus'
import httpclient from '@/apis/httpclient'

export const useBaseStore = defineStore('base', () => {
  const controlName = ref('')

  const items = ref([])

  const loading = ref(false)

  const buildUrl = () => {
    return window._apis['Business'] + '/' + controlName.value
  }

  const getListAll = async () => {
    loading.value = true
    let res = await httpclient
      .requestAsync(
        {
          url: buildUrl(),
          headers: {}
        },
        'GET'
      )
      .catch(() => {})
      .finally(() => {
        loading.value = false
      })
    if (res) {
      items.value = res.data
    }

    return res.data
  }

  const getPaging = async (payload) => {
    loading.value = true
    let res = await httpclient
      .requestAsync(
        {
          url: buildUrl() + '/GetPaging',
          headers: {},
          data: payload
        },
        'POST'
      )
      .finally(() => {
        loading.value = false
      })

    return res.data
  }

  watch(
    () => loading.value,
    (newVal) => {
      let loading = ElLoading.service({
        lock: true,
        background: 'rgba(0, 0, 0, 0.7)'
      })
      if (!newVal) {
        loading.close()
      }
    }
  )

  return {
    items,
    controlName,
    getListAll,
    getPaging
  }
})
