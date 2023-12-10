import { defineStore } from 'pinia'
import { ref, watch } from 'vue'
import { ElLoading } from 'element-plus'
import httpclient from '@/apis/httpclient'
import axios from 'axios'

export const useBaseStore = defineStore('base', () => {
  const controlName = ref('')

  const items = ref([])

  const loading = ref(false)

  const buildUrl = () => {
    return window._apis['Business'] + '/'
  }

  const getListAll = async (controlName) => {
    loading.value = true
    let res = await httpclient
      .requestAsync(
        {
          url: buildUrl() + `${controlName}`,
          headers: {}
        },
        'GET'
      )
      .catch(() => {})
      .finally(() => {
        loading.value = false
      })

    return res.data
  }

  const getPaging = async (controlName, payload) => {
    loading.value = true
    let res = await httpclient
      .requestAsync(
        {
          url: buildUrl() + `${controlName}` + '/GetPaging',
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

  const executeCommand = async (controlName, sql) => {
    loading.value = true
    let res = await httpclient
      .requestAsync(
        {
          url: buildUrl() + `${controlName}` + '/ExecuteCommand',
          headers: {},
          data: sql
        },
        'POST'
      )
      .finally(() => {
        loading.value = false
      })

    return res.data
  }

  const uploadFile = async (controlName, playload) => {
    loading.value = true
    return await axios
      .post(buildUrl() + `${controlName}` + '/UploadFile', playload, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }).finally(() => {
        loading.value = false
      })
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
    uploadFile,
    getListAll,
    getPaging,
    executeCommand
  }
})
