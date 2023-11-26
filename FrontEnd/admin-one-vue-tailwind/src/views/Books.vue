<template>
  <LayoutAuthenticated>
    <SectionMain>
      <SectionTitleLineWithButton
        :icon="mdiBookOpenPageVariantOutline"
        title="Danh sách điện tử"
        main
      >
      </SectionTitleLineWithButton>

      <el-table :data="eBooks"  style="width: 100%">
        <el-table-column prop="bookName" label="bookName" width="250" />
        <el-table-column prop="description" label="description" width="300" />
        <el-table-column prop="address" label="Address" />
      </el-table>
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

const bookStore = useBookStore()

const eBooks = ref([])

// get data eBook
bookStore.getListAll().then((result) => {
  eBooks.value = result
})

</script>
