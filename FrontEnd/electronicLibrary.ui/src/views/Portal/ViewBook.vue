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
import { useContextStore } from '@/stores/contextStore.js'
import { ElMessageBox } from 'element-plus'

const context = useContextStore()

const route = useRoute()
const router = useRouter()

const PATH_IMAGE = 'http://127.0.0.1:5173/src/assets/BookFile/'

const bookIframe = ref()

const iframeSrc = ref('')

const isShowIframe = ref(false)

const bookStore = useBookStore()

const bookId = ref()

const getBookDetail = (bookId) => {
  let sql = `SELECT
              *
            FROM view_book vb
              LEFT JOIN readhistory r
                ON vb.BookId = r.BookId
            WHERE vb.BookId = ${bookId}`

  bookStore.executeCommand('Book', sql).then((res) => {
    if (res.length) {
      iframeSrc.value = `public/src/pdfViewer/external/pdfjs-2.1.266-dist/web/viewer.html?file=${PATH_IMAGE}${res[0].FileBookID}`
      isShowIframe.value = true
      getHistoryRead(bookId)
    } else {
      router.push({
        name: 'portal'
      })
    }
  })
}

const readhistory = ref(null)

const getHistoryRead = (bookId) => {
  let userId = context.user.userId
  context
  let sql = `SELECT
            *
          FROM readhistory r
          WHERE r.BookId = ${bookId}
          AND r.UserId = ${userId}
          LIMIT 1`

  bookStore.executeCommand('Book', sql).then((res) => {
    if (res.length) {
      readhistory.value = res[0]
      if (readhistory.value.CurrentPage > 1) {
        nextTick(() => {
          ElMessageBox.confirm(
            `Bạn có muốn tiếp tục đọc ở trang ${readhistory.value.CurrentPage} ?`,
            'Thông báo',
            {
              confirmButtonText: 'Đọc tiếp',
              cancelButtonText: 'Không'
            }
          )
            .then(() => {
              setTimeout(() => {
                bookIframe.value.contentWindow.postMessage(
                  { currentPage: readhistory.value.CurrentPage, type: 'toPage', contextData: {} },
                  '*'
                )
              }, 300)
            })
            .catch(() => {})
        })
      }
    }
  })
}

const logReadHistory = (stopPage) => {
  let userId = context.user.userId

  let sqlCheckHistory = `SELECT
                          *
                        FROM readhistory r
                        WHERE r.UserId = ${userId}
                        AND r.BookId = ${bookId.value}`

  bookStore.executeCommand('Book', sqlCheckHistory).then((res) => {
    let sql = ''

    if (res.length > 0) {
      sql = `UPDATE readhistory r
          SET r.CurrentPage = ${stopPage}
          WHERE r.ReadHistoryId = ${res[0].ReadHistoryId}`
    } else {
      sql = `INSERT INTO readhistory (UserId, BookId, CurrentPage)
  VALUES (${userId}, ${bookId.value}, ${stopPage});`
    }
    bookStore.executeCommand('Book', sql)
  })
}

onMounted(() => {
  bookId.value = route.query.BookId
  getBookDetail(route.query.BookId)

  window.addEventListener('message', function (event) {
    const data = event.data
    switch (data?.type) {
      case 'stopReading':
        logReadHistory(data.currentPage)
        router.push({
          name: 'portal'
        })
        setTimeout(() => {
          location.reload()
        }, 10)

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
