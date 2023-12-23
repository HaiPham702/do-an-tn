<template>
  <div class="book-item rounded-xl">
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
    <div class="card-overlay">
      <div class="btn-card view-detail cursor-pointer mb-3" @click="$emit('showDetailPopup', data)">
        Xem chi tiết
      </div>
      <div class="btn-card read-book cursor-pointer" @click="onReadBook">Đọc sách</div>
    </div>
  </div>
  <!-- form -->

  <el-dialog v-model="dialogVisible" :title="data.BookName" width="30%" :before-close="handleClose">
    <span>This is a message</span>
  </el-dialog>
</template>
<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
const props = defineProps({
  data: {}
})

const dialogVisible = ref(false)

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

onMounted(() => {})
</script>
<style lang="scss" scope>
.book-item {
  position: relative;
  overflow: hidden;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
  width: 208px;
  height: 384px;
  transition: transform 0.2s; /* Animation */

  .card-overlay {
    display: none;
    position: absolute;
    top: 0;
    bottom: 0;
    right: 0;
    left: 0;
    backdrop-filter: blur(3px);
    flex-direction: column;
    justify-content: center;
    align-items: center;
    .btn-card {
      width: 120px;
      padding: 8px;
      border-radius: 8px;
      background: #04acf1;
      color: #fff;
      font-weight: 600;
    }
  }
  &:hover {
    .card-overlay {
      display: flex;
    }
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.5);
    img {
      transform: scale(
        1.06
      ); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */
    }
  }
  .book_img {
    height: 276px;
    overflow: hidden;
    img {
      object-fit: cover;
      transition: transform 0.2s; /* Animation */
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
