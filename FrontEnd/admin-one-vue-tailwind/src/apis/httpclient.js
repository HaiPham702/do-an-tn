import axios from 'axios'
import { useContextStore } from '@/stores/contextStore.js'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'

export const POST = 'POST'
export const PUT = 'PUT'
export const DELETE = 'DELETE'
export const GET = 'GET'

export const ApplicationJson = 'application/json'

const context = useContextStore()

class AxiosHttpClient {
  async requestPost(config, method, contenType = ApplicationJson) {
    this.requestAsync(config, POST, contenType)
  }

  async requestAsync(config, method, contenType = ApplicationJson) {
    this._processHeaders(config, contenType)
    const router = useRouter()

    config.method = method

    return await axios(config).catch((res) => {
      let status = res.response.status
      switch (status) {
        case 401:
          router.push({
            name: 'login'
          })
          break
        case 400:
          ElMessage.error(res.response.data.message)
          throw new Error(res)
        default:
          break
      }
    })
  }

  _processHeaders(config, contenType = ApplicationJson) {
    if (!config) return

    let headers = config.headers || {}
    if (!headers['Authorization']) {
      if (context.Token) {
        config.headers['Authorization'] =  `Bearer ${context.Token}` 
      }
    }

    config.headers['Content-Type'] = contenType
  }
}

export default new AxiosHttpClient()
