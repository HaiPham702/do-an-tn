<template>
  <div class="book-item rounded-xl cursor-pointer" @click="onReadBook">
    <div class="book_img">
      <img :src="urlBookCorver" />
    </div>
    <div class="text-left px-2 pt-2">
      <div class="book-title font-semibold text-base mb-1">
        <div class="title-content">
          {{ data.BookName }}
        </div>
      </div>
      <div class="author">Tác giả: {{ data.Author }}</div>
      <div class="text-xs">Nhà xuất bản: {{ data.PublisherName }}</div>
    </div>
  </div>
  <!-- form -->
</template>
<script setup>
import { computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
const props = defineProps({
  data: {}
})

const router = useRouter()

const urlBookCorver = computed(() => {
  return `src/assets/BookFile/${props.data.FileCoverBook}`
})

const onReadBook = () => {
  router.push({
    name: 'bookView',
    query: { BookId: props.data.BookId }
  })
}

onMounted(() => {
})
</script>
<style lang="scss" scope>
.book-item {
  overflow: hidden;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
  width: 208px;
  height: 384px;
  .book_img {
    height: 276px;
    overflow: hidden;
    img {
      object-fit: cover;
    }
  }
  .book-title {
    height: 38px;
    font-size: 16px;
    line-height: 19px;
    overflow: hidden;
    .title-content {
      display: -webkit-box;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
      overflow: hidden;
      white-space: pre-wrap;
    }
  }

  .author {
    font-size: 12px;
  }
}
</style>
