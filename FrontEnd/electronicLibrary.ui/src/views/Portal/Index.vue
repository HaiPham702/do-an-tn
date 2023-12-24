<template>
  <div class="portal">
    <div class="portal-info py-2 container-portal flex justify-between">
      <div class="logo flex">
        <img class="mr-4" src="@/assets/icons/LogoSchool.svg" alt="" />
        <div class="">
          <div class="font-bold text-lg">Trường Tiểu học thư viện điện tử</div>
          <div>Địa chỉ: Đ. Cầu Diễn, Minh Khai, Bắc Từ Liêm, Hà Nội</div>
        </div>
      </div>
      <div class="user-info flex items-center">
        <div class="mr-4">{{ userName }}</div>
        <el-avatar :src="mainStore.userAvatar" />
        <el-dropdown class="ml-4">
          <span class="el-dropdown-link">
            <div>
              <img src="@/assets/icons/ThreeLine.svg" />
            </div>
          </span>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item @click="logout">Đăng xuất</el-dropdown-item>
              <el-dropdown-item v-if="context.user.role == 1" @click="goAdministration"
                >Trang quản trị</el-dropdown-item
              >
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </div>
    </div>
    <!-- Danh sách nhóm loại sách -->
    <div class="list-bookGroup container-portal">
      <!-- <el-dropdown>
        <span class="el-dropdown-link">
          <div>
            <img src="@/assets/icons/ThreeLine.svg" />
          </div>
        </span>
        <template #dropdown>
          <el-dropdown-menu>
            <el-dropdown-item v-for="(item, index) in groupBook" :key="index">{{
              item.GroupName
            }}</el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown> -->
    </div>
    <div>
      <img src="@/assets/image/BannerDefault.png" />
    </div>
    <!-- Input tìm kiếm -->

    <div class="container-portal py-4">
      <div class="search-container">
        <div class="input-wrap">
          <input
            v-model="textSearch"
            class="ms-input-item flex w-full"
            placeholder="Nhập nhan đề, Tên tác giả"
            maxlength="255"
            @keydown.enter="getBooks"
          />
        </div>
        <button class="btn-search">
          <div class="text">Tìm kiếm</div>
        </button>
      </div>
    </div>
    <div class="container-portal">
      <GroupViewBook v-if="booksRead.length" title="Sách đang đọc" :data="booksRead" />

      <GroupViewBook v-for="(item, index) in books" :key="index" :data="item" />
    </div>
  </div>
</template>
<script setup>
import { computed, onMounted, ref } from 'vue'
import { useMainStore } from '@/stores/main'
import { useBookStore } from '@/stores/bussiness/bookStore.js'
import GroupViewBook from '@/views/Portal/GroupViewBook.vue'
import { useRouter } from 'vue-router'
import { useContextStore } from '@/stores/contextStore.js'

const context = useContextStore()

const mainStore = useMainStore()
const userName = computed(() => mainStore.userName)

const router = useRouter()

const bookStore = useBookStore()

const groupBook = ref([])

const booksRead = ref([])

const getBooksRead = () => {
  let sql = `SELECT
              vb.*
            FROM view_book vb
              INNER JOIN readhistory r
                ON vb.BookId = r.BookId
              WHERE r.UserId = ${context.user.userId} 
              ORDER BY r.ReadHistoryId DESC`

  bookStore.executeCommand('Book', sql).then((res) => {
    booksRead.value = res
  })
}

const getBooKGroup = () => {
  let sql = `SELECT
              b.BookGroupId,
              b.GroupName
            FROM bookgroup b
              INNER JOIN book b1
                ON b.BookGroupId = b1.BookGroupId
            GROUP BY b.BookGroupId,
                    b.GroupName
            ORDER BY COUNT(*) DESC`

  bookStore.executeCommand('Book', sql).then((res) => {
    groupBook.value = res
  })
}

const logout = () => {
  router.push({
    name: 'login'
  })
}

const goAdministration = () => {
  router.push({
    name: 'dashboard'
  })
}

const textSearch = ref('')

const groupBy = (array, key) => {
  return array.reduce((result, currentItem) => {
    // Lấy giá trị của khóa từ phần tử hiện tại
    const keyValue = currentItem[key]

    // Tạo một nhóm mới nếu chưa tồn tại
    if (!result[keyValue]) {
      result[keyValue] = []
    }

    // Thêm phần tử vào nhóm
    result[keyValue].push(currentItem)

    return result
  }, {})
}

const books = ref([])

const getBooks = () => {
  let sql = `SELECT
              b.*,
              b1.GroupName
            FROM view_book b
              INNER JOIN bookgroup b1
                ON b.BookGroupId = b1.BookGroupId
            WHERE (b.BookName LIKE '%${textSearch.value.trim()}%'
            OR b.Author LIKE '%${textSearch.value.trim()}%')
            AND b.FileBookID IS NOT NULL
            ORDER BY b1.BookGroupId`

  bookStore.executeCommand('Book', sql).then((res) => {
    books.value = groupBy(res, 'BookGroupID')
  })
}

onMounted(() => {
  getBooKGroup()
  getBooks()
  getBooksRead()
})
</script>
<style lang="scss" scope>
.container-portal {
  width: 1104px;
  margin: auto;
}

.portal {
  height: 100vh;
  margin: auto;
  .portal-info {
  }
}

.search-container {
  display: flex;
  border: 1px solid #dbdbdb;
  padding-left: 12px;
  border-radius: 12px;
  overflow: hidden;
  .input-wrap {
    flex: 1;
    input {
      border: none;
      outline: none;
    }
  }
  .btn-search {
    padding: 0 12px;
    background: #04acf1;
    border: 1px solid #04acf1;
    font-weight: 700;
    color: white;
  }
}
</style>
