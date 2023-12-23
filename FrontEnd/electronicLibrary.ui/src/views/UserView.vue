<template>
  <LayoutAuthenticated>
    <SectionMain>
      <SectionTitleLineWithButton :icon="mdiBallotOutline" title="Danh sách bạn đọc" main>
      </SectionTitleLineWithButton>
      <CardBox>
        <el-table :data="userList" style="width: 100%" :border="true" size="large" row-key="UserId">
          <el-table-column type="selection" width="55" />
          <el-table-column prop="FullName" label="Họ và tên" width="250" />
          <el-table-column prop="DateOfBrith" label="Ngày sinh" width="150">
            <template #default="scope">
              {{ formatDate(scope) }}
            </template>
          </el-table-column>
          <el-table-column prop="Gender" label="Giới tính" width="150">
            <template #default="scope">
              {{ displayGender(scope) }}
            </template>
          </el-table-column>
          <el-table-column prop="Email" label="Email" width="300" />
          <el-table-column prop="Role" label="Vai trò" width="150">
            <template #default="scope">
              {{ scope.row?.Role === 1 ? 'Quản trị viên' : 'Bạn đọc' }}
            </template>
          </el-table-column>
        </el-table>
      </CardBox>
    </SectionMain>
  </LayoutAuthenticated>
</template>

<script setup>
import { reactive, ref, onMounted } from 'vue'
import { mdiBallotOutline } from '@mdi/js'
import SectionMain from '@/components/SectionMain.vue'
import CardBox from '@/components/CardBox.vue'
import LayoutAuthenticated from '@/layouts/LayoutAuthenticated.vue'
import SectionTitleLineWithButton from '@/components/SectionTitleLineWithButton.vue'
import { useContextStore } from '@/stores/contextStore'
import { useBookStore } from '@/stores/bussiness/bookStore.js'
import moment from 'moment'

const contextStore = useContextStore()
const bookStore = useBookStore()

const userList = ref([])

const displayGender = (scope) => {
  return scope.row?.Gender == 1 ? 'Nam' : 'Nữ '
}

const formatDate = (scope) => {
  if (scope.row?.DateOfBrith) {
    const currentDate = moment(scope.row?.DateOfBrith)
    // Định dạng ngày theo định dạng mong muốn
    return currentDate.format('DD-MM-YYYY')
  }
  return '--'
}

onMounted(() => {
  let sql = `SELECT u.UserId,
            u.FullName,
            u.DateOfBrith,
            u.Gender,
            u.Email,
            u.UserName,
            u.Role from user u`

  bookStore.executeCommand('Book', sql).then((res) => {
    userList.value = res
  })
})
</script>
