<template>
  <div>
    <div v-if="showTable">
      <div class="pb-3">
        <q-btn @click="addProductClickHandler">
          Thêm Sản Phẩm
        </q-btn>
        <q-btn
          v-if="isShowDeleteSelectedProduct"
          class="!bg-red-500 ml-4 text-white"
          @click="deleteSelected"
        >
          Xóa Phần Được Chọn
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
          :selected-rows-label="getSelectedString"
          selection="multiple"
          rows-per-page-label="Bản ghi mỗi trang"
          :pagination-label="getPagingText"
        >
          <template #header-selection="scope">
            <q-checkbox v-model="scope.selected" />
          </template>
          <template #body="props">
            <q-tr :props="props">
              <q-td><q-checkbox v-model="props.selected" /></q-td>
              <q-td v-for="col in props.cols">
                <div class="text-center flex justify-center items-center">
                  <template v-if="col.name == 'Images'">
                    <img
                      :src="imageToLink(col.value)"
                      class="w-[50px] h-auto"
                    >
                  </template>
                  <template v-else-if="col.name == 'Function-button'">
                    <q-btn
                      class="!bg-sky-600 text-white mr-1"
                      @click="updateProductClickHandler(props.row)"
                    >
                      Chỉnh Sửa{{ col.value }}
                    </q-btn>
                    <q-btn
                      class="!bg-red-500 text-white"
                      @click="deleteProduct(props.row)"
                    >
                      Xóa
                    </q-btn>
                  </template>
                  <template v-else>
                    {{ col.value }}
                  </template>
                </div>
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </div>
    </div>
    <AddProduct
      v-if="showAddProduct"
      :is-update="isUpdate"
      :update-product="modifiedProduct"
      :type-list="productType"
      @changeToList="backToListHandler"
    />
  </div>
</template>

<script setup lang="ts">
import type { IProduct } from 'interface/ProductInterface';
import { Ref, computed, onMounted, ref } from 'vue';
import _ from 'lodash';
import AddProduct from 'subcomponent/AddProduct.vue';
import productRepository from 'api/productRepository';
import typeRepository from 'api/typeRepository';
import { useDialogStore } from 'src/stores/dialog';
import { DIALOG_TYPE } from 'common/enum';


const dialog = useDialogStore();


const showTable = ref(true);
const showAddProduct = ref(false);
const isUpdate = ref(false);
const modifiedProduct: Ref<IProduct> = ref();
const productType = ref([]);

const addProductClickHandler = () => {
  isUpdate.value = false;
  showAddProduct.value = true;
  showTable.value = false;
}

const updateProductClickHandler = (product) => {
  modifiedProduct.value = product;
  isUpdate.value = true;
  showAddProduct.value = true;
  showTable.value = false;
}

const backToListHandler = () => {
  showAddProduct.value = false;
  showTable.value = true;
  getAllProduct();
}

const imageToLink = (images) => {
  if (images) {
    return `https://localhost:7082/${images.split(",")[0].trim()}`;
  }
}

const data = ref([]);

const columns = [
  {
    name: 'Images',
    required: true,
    label: 'Ảnh Đại Diện',
    align: 'center',
    field: 'images',
    sortable: true
  },
  { name: 'Name', align: 'center', label: 'Tên Sản Phẩm', field: 'name', sortable: true },
  { name: 'Code', label: 'Mã Sản Phẩm', field: 'code', sortable: true, align: 'center' },
  { name: 'Price', label: 'Giá', field: 'price', align: 'center' },
  { name: 'Keyword', label: 'Từ Khóa', field: 'keyword', align: 'center' },
  { name: 'Guarantee', label: 'Bảo Hành', field: 'guarantee', align: 'center' },
  // { name: 'Color', label: 'Màu Sắc', field: 'color', sortable: true, align: 'center', sort: (a, b) => parseInt(a, 10) - parseInt(b, 10) },
  { name: 'Mounts', label: 'Số lượng', field: 'mounts', sortable: true, sort: (a, b) => parseInt(a, 10) - parseInt(b, 10), align: 'center' },
  { name: 'Function-button',label:"Khác",align:"center" }
] as any

const getPagingText = (first,end,total) =>{
  return first + "-" + end + " trên " + total + " Bản ghi";
}


const rows = computed(() => {
  const rowsValue = _.clone(data.value);
  // rowsValue.push({
  //   name: 'function-button'
  // })
  return rowsValue
})

const getAllProduct = async () => {
  const result = await productRepository.get();
  data.value = result.payload;
}

const deleteProduct = async (product) => {
    await dialog.show({
    type:DIALOG_TYPE.CONFIRM,
    header:'Thông Tin',
    message:"Bạn có muốn xóa sản phẩm này không?",
  },async()=>{
    await productRepository.delete(product.id);
    getAllProduct();
  });
}

const selected = ref([]);
const getSelectedString = () => {
  return selected.value.length === 0 ? '' : `${selected.value.length} record${selected.value.length > 1 ? 's' : ''} selected of ${rows.value.length}`
}

const isShowDeleteSelectedProduct = computed(() => {
  return selected.value.length > 0;
});

const deleteSelected = () => {
  const idList = selected.value.map((obj) => obj.id)
  productRepository.deleteRange(idList);
  getAllProduct();
}

const getAllType = async () => {
  const result = await typeRepository.get();
  productType.value = result.payload;
}

onMounted(() => {
  getAllProduct();
  getAllType();
});
</script>

<style scoped></style>