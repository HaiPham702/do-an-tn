<template>
  <div class="view-container">
    <iframe
      v-if="isShowIframe"
      ref="bookIframe"
      :src="iframeSrc"
      width="100%"
      height="100%"
      frameborder="no"
      allowfullscreen
    ></iframe>
  </div>
</template>

<script setup>
import { ref, onMounted, nextTick } from 'vue'
import { useBookStore } from '@/stores/bussiness/bookStore.js'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()

const PATH_IMAGE = 'http://127.0.0.1:5173/src/assets/BookFile/'

const bookIframe = ref()

const iframeSrc = ref('')

const isShowIframe = ref(false)

const bookStore = useBookStore()

const getBookDetail = (bookId) => {
  let sql = `SELECT
              *
            FROM view_book vb
              LEFT JOIN readhistory r 
                ON vb.BookId = r.BookId
            WHERE vb.BookId = ${bookId}`

  bookStore.executeCommand('Book', sql).then((res) => {
    iframeSrc.value = `public/src/pdfViewer/external/pdfjs-2.1.266-dist/web/viewer.html?file=${PATH_IMAGE}${res[0].FileBookID}`
    isShowIframe.value = true

    nextTick(() => {
      // window.contentWindow.postMessage(
      //   { currentPage: 0, type: 'toPage', contextData: {} },
      //   '*'
      // )
    })
  })
}

onMounted(() => {
  getBookDetail(route.query.BookId)

  window.addEventListener('message', function (event) {
    const data = event.data
    switch (data?.type) {
      case 'stopReading':
        // debugger
        router.push({
          name: 'portal'
        })
        break

      default:
        break
    }
  })
})
</script>

<style>
.view-container {
  height: 100vh;
}
</style>
