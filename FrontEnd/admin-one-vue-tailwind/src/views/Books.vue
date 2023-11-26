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
        <el-table :data="eBooks" height="calc(100% - 500px)" style="width: 100%">
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
</template>

<script setup>
import { mdiBookOpenPageVariantOutline } from '@mdi/js'
import SectionMain from '@/components/SectionMain.vue'
import LayoutAuthenticated from '@/layouts/LayoutAuthenticated.vue'
import SectionTitleLineWithButton from '@/components/SectionTitleLineWithButton.vue'
import { useBookStore } from '@/stores/bussiness/bookStore.js'
import { ref, watch } from 'vue'
import DefaltBookImg from '@/assets/icons/book_default.png'

const bookStore = useBookStore()

const eBooks = ref([])

const buildSrcCoverBook = (data) => {
  return DefaltBookImg
}

// get data eBook
bookStore.getListAll().then((result) => {
  eBooks.value = result
})
</script>

<style lang="scss" scoped>
.container-grid {
  height: calc(100% - 500px);
}
</style>
