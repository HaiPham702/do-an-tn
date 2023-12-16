<template>
  <div class="book-item rounded-xl cursor-pointer">
    <div class="book_img">
      <img src="src/assets/BookFile/152043eb-270d-42af-b35c-255da9ed399d.jpeg" />
    </div>
    <div class="text-left px-2 pt-2">
      <div class="book-title font-semibold text-base mb-1">
        <div class="title-content">
          20 đề thi trắc nghiệm Vật lí Trungdasasasasasasasasas học phổ thông
        </div>
      </div>
      <div class="author">Tác giả: Phạm Văn Hải</div>
      <div class="text-xs">Nhà xuất bản: Giáo dục</div>
    </div>
  </div>
  <!-- form -->

</template>
<script setup></script>
<style lang="scss" scope>
.book-item {
  overflow: hidden;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
  width: 208px;
  height: 384px;
  .book_img {
    height: 276px;
    overflow: hidden;
    img {
      object-fit: cover;
    }
  }
  .book-title {
    height: 38px;
    font-size: 16px;
    line-height: 19px;
    overflow: hidden;
    .title-content {
      display: -webkit-box;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
      overflow: hidden;
      white-space: pre-wrap;
    }
  }

  .author {
    font-size: 12px;
  }
}
</style>
