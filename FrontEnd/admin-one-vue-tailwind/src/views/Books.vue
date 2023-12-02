<template>
  <LayoutAuthenticated>
    <SectionMain>
      <SectionTitleLineWithButton
        :icon="mdiBookOpenPageVariantOutline"
        title="Danh sách sách điện tử"
        main
      >
      </SectionTitleLineWithButton>
      <div class="container-grid">
        <el-table :data="eBooks" height="500" style="width: 100%" @cell-dblclick="onCellDblclick">
          <el-table-column width="100" label="Ảnh bìa" class-name="cover-book">
            <template #default="scope">
              <img :src="buildSrcCoverBook(scope)" />
            </template>
          </el-table-column>
          <el-table-column prop="bookName" label="Tên sách" width="250" />
          <el-table-column prop="description" label="Mô tả" width="300" />
          <el-table-column prop="address" label="Address" />
        </el-table>
      </div>
    </SectionMain>
  </LayoutAuthenticated>
  <BookDetail ref="refDetail" :form-param="formParam"/>
</template>

<script setup>
import { mdiBookOpenPageVariantOutline } from '@mdi/js'
import SectionMain from '@/components/SectionMain.vue'
import LayoutAuthenticated from '@/layouts/LayoutAuthenticated.vue'
import SectionTitleLineWithButton from '@/components/SectionTitleLineWithButton.vue'
import { useBookStore } from '@/stores/bussiness/bookStore.js'
import { reactive, ref, watch } from 'vue'
import DefaltBookImg from '@/assets/icons/book_default.png'
import BookDetail from '@/views/BookDetail.vue'

const bookStore = useBookStore()

const eBooks = ref([])

const refDetail = ref()

const formParam = ref({
  
})

const buildSrcCoverBook = (data) => {
  return DefaltBookImg
}

// get data eBook
bookStore.getListAll().then((result) => {
  eBooks.value = result
})

const onCellDblclick = (row, column, cell, event) => {
  formParam.value = row
  refDetail.value.showBookDetail()
}
</script>

<style lang="scss" scoped>
.container-grid {
  height: calc(100% - 500px);
}
</style>
