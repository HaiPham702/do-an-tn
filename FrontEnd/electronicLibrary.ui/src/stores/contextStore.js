import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useContextStore = defineStore('context', () => {
  const Token = ref(null)

  const user = ref()

  return {
    Token,
    user
  }
})
