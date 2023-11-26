import { defineStore } from 'pinia'
import { useBaseStore } from '@/stores/baseStore.js'

export const useBookStore = defineStore('book', () => {
  const baseStore = useBaseStore()
  baseStore.controlName = 'Book'
  return {
    ...baseStore
  }
})
