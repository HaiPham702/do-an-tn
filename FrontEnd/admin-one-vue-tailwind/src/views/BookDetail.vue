<template>
  <el-dialog v-model="dialogFormVisible" title="Cập nhật sách điện tử" width="80%" draggable>
    <el-form :model="form">
      <el-form-item label="Nhan đề" :label-width="formLabelWidth">
        <el-input v-model="form.name" autocomplete="off" />
      </el-form-item>
      <el-form-item label="Tác giả" :label-width="formLabelWidth">
        <el-input v-model="form.name" autocomplete="off" />
      </el-form-item>
      <el-row>
        <el-col :span="12">
          <el-form-item label="Nhóm loại sách" :label-width="formLabelWidth">
            <el-select v-model="form.region" placeholder="Chọn nhóm loại sách">
              <el-option label="Zone No.1" value="shanghai" />
              <el-option label="Zone No.2" value="beijing" />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="Nhà xuất bản" :label-width="formLabelWidth">
            <el-select v-model="form.region" placeholder="Chọn nhà xuất bản">
              <el-option label="Zone No.1" value="shanghai" />
              <el-option label="Zone No.2" value="beijing" />
            </el-select>
          </el-form-item>
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
</template>
<script>
import { reactive, ref } from 'vue'

export default {
  props: {
    formParam: {
      type: Object,
      default: () => {}
    }
  },
  setup(props) {
    const dialogFormVisible = ref(false)

    const formLabelWidth = '130px'

    const showBookDetail = () => {
      dialogFormVisible.value = true
      props.formParam
      debugger
    }

    const form = reactive({
      name: '',
      region: '',
      date1: '',
      date2: '',
      delivery: false,
      type: [],
      resource: '',
      desc: ''
    })

    return {
      form,
      formLabelWidth,
      dialogFormVisible,
      showBookDetail
    }
  }
}
</script>

<style lang="scss" scope></style>
