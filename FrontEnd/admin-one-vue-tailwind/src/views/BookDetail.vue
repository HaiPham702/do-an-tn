<template>
  <el-dialog v-model="dialogFormVisible" title="Cập nhật sách điện tử" width="80%" draggable>
    <el-form :model="form">
      <el-row>
        <el-col :span="16">
          <el-form-item label="Nhan đề" :label-width="formLabelWidth">
            <el-input v-model="model.bookName" autocomplete="off" placeholder="Nhập nhan đề sách" />
          </el-form-item>
          <el-form-item label="Tác giả" :label-width="formLabelWidth">
            <el-input v-model="model.author" autocomplete="off" placeholder="Nhập tên tác giả" />
          </el-form-item>
          <el-form-item label="Mô tả" :label-width="formLabelWidth">
            <el-input
              v-model="model.description"
              maxlength="255"
              placeholder="Nhập mô tả về sách"
              show-word-limit
              type="textarea"
            />
          </el-form-item>
          <el-row>
            <el-col :span="12">
              <el-form-item label="Nhóm loại sách" :label-width="formLabelWidth">
                <el-select v-model="model.region" placeholder="Chọn nhóm loại sách">
                  <el-option label="Zone No.1" value="shanghai" />
                  <el-option label="Zone No.2" value="beijing" />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Nhà xuất bản" :label-width="formLabelWidth">
                <el-select v-model="model.region" placeholder="Chọn nhà xuất bản">
                  <el-option label="Zone No.1" value="shanghai" />
                  <el-option label="Zone No.2" value="beijing" />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
        </el-col>
        <el-col :span="8">
          <div class="ml-4">
            <div class="mb-2">Ảnh bìa sách</div>
            <el-upload
              v-model:file-list="fileList"
              action="#"
              list-type="picture-card"
              :auto-upload="false"
              drag
              :limit="1"
            >
              <el-icon><Plus /></el-icon>

              <template #file="{ file }">
                <div>
                  <img class="el-upload-list__item-thumbnail" :src="file.url" alt="" />
                  <span class="el-upload-list__item-actions">
                    <span
                      class="el-upload-list__item-preview"
                      @click="handlePictureCardPreview(file)"
                    >
                      <el-icon><zoom-in /></el-icon>
                    </span>
                    <span
                      v-if="!disabled"
                      class="el-upload-list__item-delete"
                      @click="handleRemove(file)"
                    >
                      <el-icon><Delete /></el-icon>
                    </span>
                  </span>
                </div>
              </template>
            </el-upload>
          </div>
        </el-col>
      </el-row>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="dialogFormVisible = false">Đóng</el-button>
        <el-button type="primary" @click="dialogFormVisible = false">Lưu</el-button>
      </span>
    </template>
  </el-dialog>
  <el-dialog v-model="dialogVisible">
    <img w-full :src="dialogImageUrl" alt="Preview Image" />
  </el-dialog>
</template>
<script>
import { reactive, ref, watch, getCurrentInstance, defineComponent, onMounted } from 'vue'
import { UploadFilled } from '@element-plus/icons-vue'
import UploadImage from '@/assets/image/upload-image.png'

export default defineComponent({
  props: {
    formParam: {
      type: Object,
      default: () => {}
    }
  },
  setup(props) {
    const { proxy } = getCurrentInstance()
    const dialogFormVisible = ref(false)

    const fileList = ref()

    const dialogImageUrl = ref('')
    const dialogVisible = ref(false)
    const disabled = ref(false)

    const handlePictureCardPreview = (file) => {
      dialogImageUrl.value = file.url
      dialogVisible.value = true
    }

    const formLabelWidth = '130px'

    const showBookDetail = () => {
      dialogFormVisible.value = true
      props.formParam
    }

    let model = reactive()

    const handleRemove = (flie) => {
      fileList.value = []
    }

    onMounted(() => {
      model = props.formParam
    })

    watch(fileList, (val) => {
      if (val.length) {
        document.querySelector('.el-upload').style.display = 'none'
      } else {
        document.querySelector('.el-upload').style.display = 'block'
      }
    })

    return {
      model,
      handleRemove,
      fileList,
      dialogVisible,
      dialogImageUrl,
      handlePictureCardPreview,
      UploadImage,
      formLabelWidth,
      dialogFormVisible,
      disabled,
      UploadFilled,
      showBookDetail
    }
  }
})
</script>

<style lang="scss" scope>
.el-upload-dragger {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
