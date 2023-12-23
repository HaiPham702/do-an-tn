<template>
  <div class="py-4">
    <div class="text-xl font-semibold pb-2">{{ title ? title : data[0].GroupName }}</div>
    <Carousel v-bind="settings" :breakpoints="breakpoints">
      <Slide v-for="(item, index) in data" :key="index">
        <CardBook :data="item" @showDetailPopup="showDetailPopup" />
      </Slide>

      <template #addons>
        <Navigation />
      </template>
    </Carousel>
  </div>
  <el-dialog v-model="dialogVisible" width="560" :before-close="handleClose">
    <div class="detail-container">
      <div class="detail-row mb-6">
        <div class="book_img">
          <img :src="`src/assets/BookFile/${dataBookDetail.FileCoverBook}`" />
        </div>
        <div class="ml-6 text-base">
          <div class="font-bold">
            {{ dataBookDetail.BookName }}
          </div>
          <div class="author my-2">Tác giả: {{ dataBookDetail.Author }}</div>
          <div class="">Nhà xuất bản: {{ dataBookDetail.PublisherName }}</div>
          <div class="my-2">Thể loại: {{ data[0].GroupName }}</div>
          <div class="">Năm xuất bản: {{ data[0].GroupName }}</div>
          <div class="read-book" @click="onReadBook">Đọc sách</div>
        </div>
      </div>
      <div class="text-base">Mô tả: {{ dataBookDetail.Description }}</div>
    </div>
  </el-dialog>
</template>
<script setup>
import { Carousel, Navigation, Slide } from 'vue3-carousel'
import 'vue3-carousel/dist/carousel.css'
import { ref, onMounted } from 'vue'
import CardBook from '@/views/Portal/CardBook.vue'
import { useRouter } from 'vue-router'


const props = defineProps({
  data: {},
  title: null
})

const dialogVisible = ref(false)
const router = useRouter()


const dataBookDetail = ref({})

const onReadBook = () => {
  router.push({
    name: 'bookView',
    query: { BookId: dataBookDetail.value.BookId }
  })
}

const showDetailPopup = (data) => {
  dataBookDetail.value = data
  dialogVisible.value = true
}

onMounted(() => {
  props
})

const settings = ref({
  itemsToShow: 1,
  snapAlign: 'center'
})

const breakpoints = ref({
  // 700px and up
  700: {
    itemsToShow: 4,
    snapAlign: 'center'
  },
  // 1024 and up
  1024: {
    itemsToShow: 5,
    snapAlign: 'start'
  }
})
</script>
<style lang="scss" scope>
.detail-container {
  .detail-row {
    display: flex;
  }

  .read-book {
    margin-top: 24px;
    text-align: center;
    width: 120px;
    padding: 8px;
    border-radius: 8px;
    background: #04acf1;
    color: #fff;
    font-weight: 600;
    cursor: pointer;
  }
  .book_img {
    width: 200px;
    height: 280px;
    overflow: hidden;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
    border-radius: 8px;
    img {
      object-fit: cover;
    }
  }
}
.carousel__track {
  margin: 12px 0;
}
</style>
