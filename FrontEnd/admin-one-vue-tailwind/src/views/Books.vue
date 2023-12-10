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
        <el-table :data="eBooks" height="500" style="width: 100%" :border="true" size="large">
          <el-table-column type="selection" width="55" />
          <el-table-column width="100" label="Ảnh bìa" class-name="cover-book">
            <template #default="scope">
              <img :src="buildSrcCoverBook(scope)" />
            </template>
          </el-table-column>
          <el-table-column prop="BookName" label="Tên sách" width="250" />
          <el-table-column prop="Description" label="Mô tả" width="300" />
          <el-table-column prop="Author" label="Tác giả" />
          <el-table-column prop="GroupName" label="Nhóm loại sách" width="150" />
          <el-table-column width="140">
            <template #default="scope">
              <el-button size="small" @click="onOpentFormEdit(scope.$index, scope.row)"
                >Sửa</el-button
              >
              <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)"
                >Xóa</el-button
              >
            </template>
          </el-table-column>
        </el-table>
        <div class="mt-3 flex justify-between">
          <div>
            Tổng:
            <span class="font-bold">
              {{ state.totalBook }}
            </span>
          </div>
          <el-pagination
            v-model:current-page="state.currentPage"
            v-model:page-size="state.pageSize"
            :page-sizes="[10, 20, 30, 40]"
            :small="small"
            :disabled="disabled"
            :background="background"
            layout="sizes, prev, pager, next"
            :total="state.totalBook"
            @size-change="handlePageSizeChange"
            @current-change="handleCurrentPageChange"
          />
        </div>
      </div>
    </SectionMain>
  </LayoutAuthenticated>
  <BookDetail ref="refDetail" :form-param="formParam" @reloadData="loadData" />
</template>

<script setup>
import { mdiBookOpenPageVariantOutline } from '@mdi/js'
import SectionMain from '@/components/SectionMain.vue'
import LayoutAuthenticated from '@/layouts/LayoutAuthenticated.vue'
import SectionTitleLineWithButton from '@/components/SectionTitleLineWithButton.vue'
import { useBookStore } from '@/stores/bussiness/bookStore.js'
import { ref, onMounted, reactive } from 'vue'
import DefaltBookImg from '@/assets/icons/book_default.png'
import BookDetail from '@/views/BookDetail.vue'
import { ElMessage, ElMessageBox } from 'element-plus'

const state = reactive({
  totalBook: 0,
  pageSize: 20,
  currentPage: 1
})

const bookStore = useBookStore()

const eBooks = ref([])

const refDetail = ref()

const formParam = ref({})

const buildSrcCoverBook = (data) => {
  return DefaltBookImg
}

const handlePageSizeChange = (num) => {
  state.pageSize = num
  loadData()
}

const handleCurrentPageChange = (num) => {
  state.currentPage = num
  loadData()
}
/**
 * get books
 */
const loadData = () => {
  // get data eBook
  let payload = {
    columns: '',
    sort: 'BookName',
    filter: null,
    limt: state.pageSize,
    skip: state.pageSize * (state.currentPage - 1)
  }
  bookStore.getPaging('Book', payload).then((result) => {
    if (result) {
      eBooks.value = result.data
      state.totalBook = result.total
    }
  })
}

onMounted(() => {
  loadData()
})

const onOpentFormEdit = (index, rowData) => {
  formParam.value = rowData
  refDetail.value.showBookDetail()
  refDetail.value.editMode = 2
}

const handleDelete = (index, rowData) => {
  let sql = `DELETE FROM book WHERE BookId = ${rowData.BookId}`

  ElMessageBox.confirm(`Bạn có muốn xóa sách ${rowData.BookName}?`, 'Thông báo', {
    confirmButtonText: 'Xóa',
    cancelButtonText: 'Đóng',
    type: 'warning'
  }).then(() => {
    bookStore.executeCommand('Book', sql).then((res) => {
      ElMessage({
        type: 'success',
        message: 'Xóa thành công'
      })
      loadData()
    })
  })
}
</script>

<style lang="scss" scoped>
.container-grid {
  height: calc(100% - 500px);
}
</style>
