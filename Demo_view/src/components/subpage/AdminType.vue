<template>
  <div>
    <div v-if="showTable">
      <div class="pb-3">
        <q-btn @click="addTypeClickHandler">
          Thêm Phân loại
        </q-btn>
      </div>
      <div>
        <q-table
          v-model:selected="selected"
          flat
          bordered
          :columns="columns"
          :rows="rows"
          row-key="name"
          class="!h-full"
          selection="multiple"
          rows-per-page-label="Bản ghi mỗi trang"
          :pagination-label="getPagingText"
        >
          <template #header-selection="scope">
            Ảnh
          </template>
          <template #body="props">
            <q-tr :props="props">
              <q-td v-for="col in props.cols">
                <div class="text-center flex justify-center items-center">
                  <template v-if="col.name == 'Function-button'">
                    <q-btn
                      class="!bg-sky-600 text-white mr-4"
                      @click="UpdateType(props.row)"
                    >
                      Chỉnh Sửa
                    </q-btn>
                    <q-btn
                      class="!bg-red-500 text-white"
                      @click="deleteType(props.row)"
                    >
                      Xóa
                    </q-btn>
                  </template>
                  <template v-else-if="col.name == 'Images'">
                    <img
                      :src="imageToLink(col.value)"
                      class="w-[50px] h-auto"
                    >
                  </template>
                  <template v-else>
                    {{ col.value }}
                  </template>
                </div>
              </q-td>
              <q-td class="w-[24x] text-center">
                <q-checkbox
                  v-model="props.row.isShowInLandingPage"
                  @click="changeEvent(props.row.id,props.row.isShowInLandingPage)"
                />
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </div>
    </div>
    <AddType
      v-if="showAddType"
      :is-update="isUpdate"
      :update-type="modifiedType"
      @changeToList="backToListHandler"
    />
  </div>
</template>

<script setup lang="ts">
import { computed, nextTick, onMounted, ref, watch, watchEffect } from 'vue';
import _ from 'lodash';
import AddType from 'subcomponent/AddType.vue';
import productRepository from 'api/productRepository';
import typeRepository from 'api/typeRepository';
import { isUpdateExpression } from '@babel/types';
import { useDialogStore } from 'src/stores/dialog';
import { DIALOG_TYPE } from 'common/enum';


const isUpdate = ref(false);

const showTable = ref(true);
const showAddType = ref(false);
const modifiedType = ref({});
const dialog = useDialogStore();


const addTypeClickHandler = () => {
  isUpdate.value = false;
  showAddType.value = true;
  showTable.value = false;
}
const selected = ref();

const backToListHandler = async() => {
  showAddType.value = false;
  showTable.value = true;
  await getAllType();
}

const data = ref([]);

const columns = [
  {
    name: 'Images',
    required: true,
    label: 'Tên Phân Loại',
    align: 'center',
    field: 'images',
  },
  { name: 'Name', align: 'center', label: 'Khác', field: 'name' },
  { name: 'Function-button', align: 'center', label: 'Cờ Hiện Thị Ở Landing Page'},
  
] as any

const rows = computed(() => {
  const rowsValue = _.clone(data.value);
  // rowsValue.push({
  //   name: 'function-button'
  // })
  return rowsValue
})

const getAllType = async () => {
  const result = await typeRepository.get();
  data.value = result.payload;
}

const deleteType = async (type) => {

  const value = await dialog.show({
    type:DIALOG_TYPE.CONFIRM,
    header:'Thông Tin',
    message:"Bạn có muốn xóa phân loại sản phẩm này không?",
  },async()=>{
    await typeRepository.delete(type.id);
    getAllType();
  });

}


const deleteSelected = () =>{
  const idList = selected.value.map((obj)=> obj.id)
  productRepository.deleteRange(idList);
  getAllType();
}

const imageToLink = (images) => {
  if (images) {
    return `https://localhost:7082/${images.split(",")[0].trim()}`;
  }
}

const getPagingText = (first,end,total) =>{
  return first + "-" + end + " trên " + total + " Bản ghi";
}

const UpdateType = (type) =>{
  modifiedType.value = type; 
  isUpdate.value = true;
  showAddType.value = true;
  showTable.value = false;
}

const changeEvent = async(id,isShow) =>{
  await typeRepository.updateFlagShow(id,isShow)
  getAllType();
}

onMounted(() => {
  getAllType();
})

</script>

<style scoped></style>
