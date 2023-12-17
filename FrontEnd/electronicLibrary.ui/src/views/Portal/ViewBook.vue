<template>
  <div class="view-container">
    <iframe :src="iframeSrc" width="100%" height="100%" frameborder="0"></iframe>
  </div>
</template>

<script setup>
import {ref} from 'vue'

const PATH_IMAGE = 'http://127.0.0.1:5173/src/assets/BookFile/'

const iframeSrc = ref(`public/src/pdfViewer/external/pdfjs-2.1.266-dist/web/viewer.html?file=${PATH_IMAGE}1dbac8c9-9621-47b2-b987-7edb39f34dc0.pdf`)
</script>

<style>
.view-container {
    height: 100vh;
}
</style>
