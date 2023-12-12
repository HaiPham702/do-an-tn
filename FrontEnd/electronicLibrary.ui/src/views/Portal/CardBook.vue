<template>
    <div class="book-item rounded-xl">
        <div class="book_img">
            <img src="src/assets/BookFile/152043eb-270d-42af-b35c-255da9ed399d.jpeg">
        </div>
    </div>
</template>
<script setup>

</script>
<style lang="scss" scope>
    .book-item {
        overflow: hidden;
        box-shadow: 0 2px 6px rgba(0,0,0,.08);
        width: 208px;
        height: 384px;
        .book_img {
            height: 276px;
            img {
                object-fit: cover;
            }
        }
    }
</style>