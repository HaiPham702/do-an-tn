<template>
  <el-dialog
    v-if="dialogFormVisible"
    v-model="dialogFormVisible"
    :title="formTitle"
    width="80%"
    draggable
  >
    <el-form ref="formRef" :model="formData">
      <el-row>
        <el-col :span="16">
          <el-form-item
            label="Nhan đề"
            :label-width="formLabelWidth"
            prop="BookName"
            :rules="{
              required: true,
              message: 'Nhan đề không được phép bỏ trống',
              trigger: 'blur'
            }"
          >
            <el-input
              v-model="formData.BookName"
              autocomplete="off"
              placeholder="Nhập nhan đề sách"
            />
          </el-form-item>
          <el-form-item label="Tác giả" :label-width="formLabelWidth">
            <el-input v-model="formData.Author" autocomplete="off" placeholder="Nhập tên tác giả" />
          </el-form-item>
          <el-form-item label="Mô tả" :label-width="formLabelWidth">
            <el-input
              v-model="formData.Description"
              maxlength="255"
              placeholder="Nhập mô tả về sách"
              show-word-limit
              type="textarea"
            />
          </el-form-item>
          <el-row>
            <el-col :span="12">
              <el-form-item
                label="Nhóm loại sách"
                :label-width="formLabelWidth"
                prop="BookGroupID"
                :rules="{
                  required: true,
                  message: 'Nhan đề không được phép bỏ trống',
                  trigger: 'blur'
                }"
              >
                <el-select v-model="formData.BookGroupID" placeholder="Chọn nhóm loại sách">
                  <el-option
                    v-for="(item, index) in bookgroups"
                    :key="index"
                    :label="item.groupName"
                    :value="item.bookGroupID"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="Nhà xuất bản" :label-width="formLabelWidth">
                <el-select v-model="formData.PublisherId" placeholder="Chọn nhà xuất bản">
                  <el-option
                    v-for="(item, index) in publishers"
                    :key="index"
                    :label="item?.publisherName"
                    :value="item.publisherID"
                  />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
        </el-col>
        <el-col :span="8">
          <div class="ml-4">
            <div class="mb-2">Ảnh bìa sách</div>
            <el-upload
              v-model:file-list="fileCover"
              action="#"
              list-type="picture-card"
              :auto-upload="false"
              drag
              :limit="1"
              accept="image/*"
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

            <FormFilePicker
              v-model="fileBook"
              class="mt-3"
              label="Tải file sách"
              accept=".pdf"
              :file-name="fileNameUser"
              @changeFile="changeFile = true"
            />
          </div>
        </el-col>
      </el-row>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="dialogFormVisible = false">Đóng</el-button>
        <el-button type="primary" @click="submitForm">Lưu</el-button>
      </span>
    </template>
  </el-dialog>
  <el-dialog v-model="dialogVisible">
    <img w-full :src="dialogImageUrl" alt="Preview Image" />
  </el-dialog>
</template>
<script lang="ts">
import FormFilePicker from '@/components/FormFilePicker.vue'
import { ref, watch, getCurrentInstance, defineComponent, onMounted, computed, nextTick } from 'vue'
import { UploadFilled } from '@element-plus/icons-vue'
import UploadImage from '@/assets/image/upload-image.png'
import { usePublisherStore } from '@/stores/bussiness/publisherStore.js'
import { ElMessage } from 'element-plus'
const PATH_IMAGE = 'src/assets/BookFile/'

export default defineComponent({
  components: {
    FormFilePicker
  },
  props: {
    formParam: {
      type: Object,
      default: () => {}
    }
  },
  emits: ['reloadData'],

  setup(props, { emit }) {
    const { proxy } = getCurrentInstance()

    const formTitle = ref('Cập nhật sách điện tử')

    const formData = ref({})

    const changeFile = ref(false)

    const publisherStore = usePublisherStore()

    const editMode = computed(() => {
      if (formData.value.BookId) {
        return 2
      } else {
        return 1
      }
    })

    const fileBook = ref()

    const formRef = ref()

    const fileCover = ref()

    const submitData = async () => {
      if (fileBook.value) {
        if (changeFile.value || editMode.value == 1) {
          publisherStore
            .uploadFile('Book', {
              file: fileBook.value,
              imageCover: fileCover.value ? fileCover.value[0]?.raw : null
            })
            .then((res) => {
              switch (editMode.value) {
                case 1: // insert
                  submitInsert(res.data)

                  break
                case 2: // update
                  submitUpdate(res.data)
                  break
              }
            })
        } else {
          switch (editMode.value) {
            case 1: // insert
              // submitInsert()
              break
            case 2: // update
              submitUpdate()
              break
          }
        }
      } else {
        ElMessage({
          type: 'warning',
          message: 'File sách không được để trống'
        })
      }
    }

    const submitInsert = (fileData) => {
      let data = formData.value
      let sql = `INSERT INTO book (BookName, Description, BookGroupId, Author, PublisherId, IsActive, FileBookID, FileCoverBook, FileName)
                 VALUES ('${data.BookName}', '${data.Description ? data.Description : ''}', ${
                   data.BookGroupID
                 }, '${data.Author ? data.Author : ''}', ${
                   data.PublisherId ? data.PublisherId : null
                 }, 1, '${fileData.fileName}', '${fileData.imageCoverName}', '${
                   fileData.fileNameUser
                 }');`
      debugger
      publisherStore.executeCommand('Publisher', sql).then((res) => {
        ElMessage({
          type: 'success',
          message: 'Thêm mới thành công'
        })

        emit('reloadData')

        dialogFormVisible.value = false
      })
    }

    const submitUpdate = (fileData) => {
      let data = formData.value
      let sql = `UPDATE book b SET BookName = '${
        data.BookName ? data.BookName : ''
      }',Description = '${data.Description ? data.Description : ''}',Author = '${
        data.Author ? data.Author : ''
      }',BookGroupID = ${data.BookGroupID}
      ${
        fileData
          ? `,PublisherId = ${data.PublisherId},FileBookID = '${fileData.fileName}',FileCoverBook = '${fileData.imageCoverName}',FileName = '${fileData.fileNameUser}' `
          : ''
      }
      WHERE BookId = ${data.BookId};`

      publisherStore.executeCommand('Publisher', sql).then((res) => {
        ElMessage({
          type: 'success',
          message: 'Cập nhật thành công'
        })

        emit('reloadData')

        dialogFormVisible.value = false
      })
    }

    const submitForm = () => {
      if (!formRef.value) return
      formRef.value.validate((valid) => {
        if (valid) {
          submitData()
        } else {
          console.log('error submit!')
          return false
        }
      })
    }

    const fileNameUser = ref(null)

    const formLabelWidth = '130px'

    const dialogFormVisible = ref(false)

    const dialogImageUrl = ref('')
    const dialogVisible = ref(false)
    const disabled = ref(false)

    const handlePictureCardPreview = (file) => {
      dialogImageUrl.value = file.url
      dialogVisible.value = true
    }

    const showBookDetail = () => {
      dialogFormVisible.value = true
    }

    const handleRemove = (flie) => {
      fileCover.value = []
    }

    const publishers = ref([])

    const bookgroups = ref([])

    onMounted(() => {
      formData.value = props.formParam

      publisherStore.getListAll('Publisher').then((res) => {
        publishers.value = res
      })
      publisherStore.getListAll('BookGroup').then((res) => {
        bookgroups.value = res
      })
    })

    watch(fileCover, (val) => {
      if (val.length) {
        let imageMimes = ['image/jpeg', 'image/png', 'image/gif', 'image/svg+xml']

        if (!imageMimes.includes(val[0].raw?.type) && !val[0].url) {
          ElMessage({
            type: 'error',
            message: 'File không đúng định dạng.'
          })

          fileCover.value = []
        }
        nextTick(() => {
          if (document.querySelector('.el-upload')) {
            document.querySelector('.el-upload').style.display = 'none'
          }
        })
      } else {
        if (document.querySelector('.el-upload')) {
          document.querySelector('.el-upload').style.display = 'block'
        }
      }
    })

    watch(
      () => props.formParam,
      (val) => {
        if (val) {
          formData.value = val
          if (val?.FileCoverBook) {
            fileCover.value = [
              {
                name: val.FileCoverBook,
                url: `${PATH_IMAGE}${val.FileCoverBook}`
              }
            ]
          }
          if (val.FileBookID) {
            let filePath = `${PATH_IMAGE}${val.FileCoverBook}`
            var file = new File([filePath], filePath)

            fileBook.value = file
            if (val.FileName) {
              fileNameUser.value = val.FileName
            }
          }
          changeFile.value = false
        }

        if (editMode.value == 1) {
          formTitle.value = 'Thêm sách điện tử'
        } else {
          formTitle.value = 'Cập nhật sách điện tử'
        }
      }
    )

    watch(dialogFormVisible, (val) => {
      if (!val) {
        fileCover.value = []
        fileBook.value = null
        fileNameUser.value = null
      } else if (formData.value) {
        let data = formData.value
        if (data?.FileCoverBook) {
          fileCover.value = [
            {
              name: data.FileCoverBook,
              url: `${PATH_IMAGE}${data.FileCoverBook}`
            }
          ]
        }
        if (data.FileBookID) {
          let filePath = `${PATH_IMAGE}${data.FileCoverBook}`
          var file = new File([filePath], filePath)

          fileBook.value = file
          if (data.FileName) {
            fileNameUser.value = data.FileName
          }
        }
      }
    })

    return {
      formTitle,
      changeFile,
      fileNameUser,
      fileBook,
      formRef,
      submitForm,
      bookgroups,
      publishers,
      formData,
      fileCover,
      dialogVisible,
      dialogImageUrl,
      UploadImage,
      formLabelWidth,
      dialogFormVisible,
      disabled,
      handleRemove,
      UploadFilled,
      showBookDetail,
      handlePictureCardPreview
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
