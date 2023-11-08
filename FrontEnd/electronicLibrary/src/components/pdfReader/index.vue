<template>
  <div>
    <iframe
      :src="src"
    />
  </div>
</template>
<script lang="ts">
import { ref } from 'vue';
export default {  
  name: 'BookReader',
  setup() {
    const src = ref<string>('http://127.0.0.1:5501/public/src/pdfViewer/external/pdfjs-2.1.266-dist/web/viewer.html')
    return {
      src
    }
  },
};
</script>
<style lang="scss" scope></style>
