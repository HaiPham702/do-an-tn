<script setup>
import { computed, ref, onMounted } from 'vue'
import {
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

onMounted(() => {
  // let sql = `SELECT
  //               b.BookId,
  //               b.BookName,
  //               COUNT(*) AS ReadCount
  //               FROM book b
  //               INNER JOIN readhistory r
  //                   ON b.BookId = r.BookId
  //               GROUP BY b.BookName,
  //                       b.BookId
  //               ORDER BY ReadCount DESC;`
  // bookStore.executeCommand('Book', sql).then((res) => {
  //   debugger
  // })
})
</script>

<template>
  <LayoutAuthenticated>
    <SectionMain>
      <SectionTitleLineWithButton :icon="mdiChartTimelineVariant" title="Tổng quan" main>
      </SectionTitleLineWithButton>

      <div class="grid grid-cols-1 gap-6 lg:grid-cols-2 mb-6">
        <CardBoxWidget
          trend-type="up"
          color="text-emerald-500"
          :icon="mdiAccountMultiple"
          :number="512"
          label="Bạn đọc"
        />
        <CardBoxWidget
          color="text-blue-500"
          :icon="mdiBookOpenPageVariantOutline"
          :number="7770"
          label="Đầu sách"
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
