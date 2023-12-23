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
          label: 'Số sách đang đọc',
          data: [],
          backgroundColor: ['#49C7CF']
        }
      ]
    })

    onMounted(() => {
      let sql = `SELECT
                  u.UserId,
                  u.FullName,
                  COUNT(*) AS TotalRead
                FROM user u
                  INNER JOIN readhistory r
                    ON u.UserId = r.UserId
                GROUP BY u.UserId,
                        u.FullName
                ORDER BY TotalRead DESC;`
      loadChart.value = false

      bookStore.executeCommand('Book', sql).then((res) => {
        if (res.length) {
          chartData.value.labels = res.map((n) => n.FullName)
          chartData.value.datasets[0].data = res.map((n) => n.TotalRead)
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
