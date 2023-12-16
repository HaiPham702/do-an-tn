<template>
  <div class="py-4">
    <div class="text-xl font-semibold pb-2">Sách có lượt xem nhiều nhất</div>
    <Carousel v-bind="settings" :breakpoints="breakpoints">
      <Slide v-for="slide in 10" :key="slide">
        <CardBook />
      </Slide>

      <template #addons>
        <Navigation />
      </template>
    </Carousel>
  </div>
</template>
<script setup>
import { Carousel, Navigation, Slide } from 'vue3-carousel'
import 'vue3-carousel/dist/carousel.css'
import { ref } from 'vue'
import CardBook from '@/views/Portal/CardBook.vue'

const settings = ref({
  itemsToShow: 1,
  snapAlign: 'center'
})

const breakpoints = ref({
  // 700px and up
  700: {
    itemsToShow: 4,
    snapAlign: 'center'
  },
  // 1024 and up
  1024: {
    itemsToShow: 5,
    snapAlign: 'start'
  }
})
</script>
<style lang="scss" scope>
.carousel__track {
    margin: 12px 0;
}
</style>
