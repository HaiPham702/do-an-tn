<script setup>
import { computed, ref, onMounted } from 'vue'
import {
  mdiFileAlertOutline,
  mdiAccountMultiple,
  mdiChartTimelineVariant,
  mdiReload,
  mdiPoll,
  mdiBookOpenPageVariantOutline,
  mdiAccountEdit
} from '@mdi/js'
import SectionMain from '@/components/SectionMain.vue'
import CardBoxWidget from '@/components/CardBoxWidget.vue'
import BaseButton from '@/components/BaseButton.vue'
import LayoutAuthenticated from '@/layouts/LayoutAuthenticated.vue'
import SectionTitleLineWithButton from '@/components/SectionTitleLineWithButton.vue'
import { useBookStore } from '@/stores/bussiness/bookStore.js'
import BookReadChart from '@/components/Charts/Barchart.vue'
import UserReadChart from '@/components/Charts/UserReadChart.vue'

const bookStore = useBookStore()

const totalUser = ref(0)
const totalBook = ref(0)

const getTotalUser = () => {
  let sql = `SELECT
              COUNT(*) as Total
            FROM user u`
  bookStore.executeCommand('Book', sql).then((res) => {
    totalUser.value = res[0].Total
  })
}

const getTotalBook = () => {
  let sql = `SELECT
              COUNT(*) as Total
            FROM book`
  bookStore.executeCommand('Book', sql).then((res) => {
    totalBook.value = res[0].Total
  })
}

const totalBookNotAttachment = ref(0)

const getTotalBookNotAttachment = () => {
  let sql = `SELECT
            COUNT(*) AS Total
          FROM view_book vb
          WHERE vb.FileBookID IS NULL
          OR vb.FileBookID = ''`
  bookStore.executeCommand('Book', sql).then((res) => {
    totalBookNotAttachment.value = res[0].Total
  })
}

onMounted(() => {
  getTotalUser()
  getTotalBook()
  getTotalBookNotAttachment()
})
</script>

<template>
  <LayoutAuthenticated>
    <SectionMain>
      <SectionTitleLineWithButton :icon="mdiChartTimelineVariant" title="Tổng quan" main>
      </SectionTitleLineWithButton>

      <div class="grid grid-cols-1 gap-6 lg:grid-cols-3 mb-6">
        <CardBoxWidget
          trend-type="up"
          color="text-emerald-500"
          :icon="mdiAccountMultiple"
          :number="totalUser"
          label="Bạn đọc"
        />
        <CardBoxWidget
          color="text-blue-500"
          :icon="mdiBookOpenPageVariantOutline"
          :number="totalBook"
          label="Đầu sách"
        />
        <CardBoxWidget
          color="text-red-500"
          :icon="mdiFileAlertOutline"
          :number="totalBookNotAttachment"
          label="Sách chưa được gắn File"
        />
      </div>
      <div class="grid grid-cols-1 gap-6 lg:grid-cols-2 mb-6">
        <div>
          <SectionTitleLineWithButton :icon="mdiPoll" title="Sách có lượt đọc nhiều nhất">
            <BaseButton :icon="mdiReload" color="whiteDark" @click="fillChartData" />
          </SectionTitleLineWithButton>
          <BookReadChart />
        </div>
        <div>
          <SectionTitleLineWithButton :icon="mdiAccountEdit" title="Bạn đọc chăm chỉ nhất">
            <BaseButton :icon="mdiReload" color="whiteDark" @click="fillChartData" />
          </SectionTitleLineWithButton>
          <UserReadChart />
        </div>
      </div>
    </SectionMain>
  </LayoutAuthenticated>
</template>
