<template>
  <Bar v-if="loadChart" id="my-chart-id" :options="chartOptions" :data="chartData" />
</template>

<script>
import { Bar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'
import { ref, onMounted } from 'vue'
import { useBookStore } from '@/stores/bussiness/bookStore.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export default {
  name: 'BarChart',
  components: { Bar },

  setup(props) {
    const bookStore = useBookStore()

    const loadChart = ref(false)

    const chartOptions = ref({
      responsive: true,
      options: {
        scales: {
          y: {
            ticks: {
              stepSize: 1 // Đặt giá trị chia nhỏ nhất của trục y là 1
            },
            stacked: true
          }
        }
      }
    })

    const chartData = ref({
      labels: [],
      datasets: [
        {
          label: 'Lượt đọc',
          data: [],
          backgroundColor: ['rgba(54, 162, 235, 0.2)']
        }
      ]
    })

    onMounted(() => {
      let sql = `SELECT
                b.BookId,
                b.BookName,
                COUNT(*) AS ReadCount
                FROM book b
                INNER JOIN readhistory r
                    ON b.BookId = r.BookId
                GROUP BY b.BookName,
                        b.BookId
                ORDER BY ReadCount DESC
                LIMIT 6;`
      loadChart.value = false

      bookStore.executeCommand('Book', sql).then((res) => {
        if (res.length) {
          chartData.value.labels = res.map((n) => n.BookName)
          chartData.value.datasets[0].data = res.map((n) => n.ReadCount)
          loadChart.value = true
        }
      })
    })

    return {
      loadChart,
      chartData,
      chartOptions
    }
  }
}
</script>
