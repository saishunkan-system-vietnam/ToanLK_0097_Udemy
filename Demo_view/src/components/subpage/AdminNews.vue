<template>
  <div>
    <div v-if="showTable">
      <div class="pb-3">
        <q-btn @click="addProductClickHandler">
          Thêm Mới
        </q-btn>
        <q-btn
          v-if="isShowDeleteSelectedProduct"
          class="!bg-red-500 ml-4 text-white"
          @click="deleteSelected"
        >
          Xóa phần được chọn
        </q-btn>
      </div>
      <div>
        <q-table
          v-model:selected="selected"
          flat
          bordered
          :columns="columns"
          :rows="rows"
          row-key="id"
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
                      Update{{ col.value }}
                    </q-btn>
                    <q-btn
                      class="!bg-red-500 text-white"
                      @click="deleteProduct(props.row)"
                    >
                      Delete
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
    <AddNews
      v-if="showAddProduct"
      :is-update="isUpdate"
      :update-product="modifiedNews"
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
import newsRepository from 'repository/newsRepository';
import AddNews from 'subcomponent/AddNews.vue';

const showTable = ref(true);
const showAddProduct = ref(false);
const isUpdate = ref(false);
const modifiedNews = ref({});
const productType = ref([]);

const addProductClickHandler = () => {
  isUpdate.value = false;
  showAddProduct.value = true;
  showTable.value = false;
}

const getPagingText = (first,end,total) =>{
  return first + "-" + end + " trên " + total + " trang";
}


const updateProductClickHandler = (product) => {
  modifiedNews.value = product;
  isUpdate.value = true;
  showAddProduct.value = true;
  showTable.value = false;
}

const backToListHandler = () => {
  showAddProduct.value = false;
  showTable.value = true;
  getAllNews();
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
    label: 'image',
    align: 'center',
    field: 'images',
    sortable: true
  },
  { name: 'Name', align: 'center', label: 'Name', field: 'title', sortable: true },
  { name: 'Function-button' }
] as any

const rows = computed(() => {
  const rowsValue = _.clone(data.value);
  // rowsValue.push({
  //   name: 'function-button'
  // })
  return rowsValue
})

const getAllNews = async () => {
  const result = await newsRepository.get();
  data.value = result.payload;
}

const deleteProduct = async (news) => {
  await newsRepository.delete(news.id);
  getAllNews();

}

const selected = ref([]);
const getSelectedString = () => {
  return selected.value.length === 0 ? '' : `${selected.value.length} record${selected.value.length > 1 ? 's' : ''} selected of ${rows.value.length}`
}

const isShowDeleteSelectedProduct = computed(() => {
  return selected.value.length > 0;
});

const deleteSelected = async () => {
  const idList = selected.value.map((obj) => obj.id)
  await newsRepository.deleteRange(idList);
  getAllNews();
}

onMounted(() => {
  getAllNews();
});
</script>

<style scoped></style>