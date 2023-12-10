import { defineStore } from 'pinia'
import { useBaseStore } from '@/stores/baseStore.js'

export const usePublisherStore = defineStore('publisher', () => {
  const baseStore = useBaseStore()
  baseStore.controlName = 'Publisher'
  return {
    ...baseStore
  }
})
