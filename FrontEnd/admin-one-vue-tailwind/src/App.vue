<script setup>
import { RouterView } from 'vue-router'
import { useContextStore } from '@/stores/contextStore.js'
import { useMainStore } from './stores/main';
const contextStore = useContextStore()
const mainStore = useMainStore()

const context = localStorage.getItem("context");
if(context) {
  contextStore.user = JSON.parse(context)
  contextStore.Token = contextStore.user?.token
  mainStore.setUser({
    name: contextStore.user?.fullName,
    email: contextStore.user?.email,
  })
}

</script>

<template>
  <RouterView/>
</template>
