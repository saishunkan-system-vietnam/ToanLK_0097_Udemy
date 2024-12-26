<template>
  <div>
    <div class="text-2xl text-[#8071b3] py-2">
      Accept Order
    </div>
    <div v-if="showTable">
      <div>
        <q-table
          flat
          bordered
          :columns="columns"
          :rows="rows"
          row-key="name"
          class="!h-full"
        >
          <template #header-selection="scope">
            <q-checkbox v-model="scope.selected" />
          </template>
          <template #body="props">
            <q-tr v-if="isAccountContain(props.row.accountId)">
              <q-td colspan="100%">
                <div class="text-left">
                  {{ props.row.account.email }}
                </div>
              </q-td>
            </q-tr>
            <q-tr :props="props">
              <q-td v-for="col in props.cols">
                <div class=" ">
                  <template v-if="col.name == 'image'">
                    <img
                      :src="imageToLink(col.value)"
                      class="w-[50px] h-auto"
                    >
                  </template>
                  <template v-else-if="col.name == 'Function-button'">
                    <div class="flex items-center justify-center">
                      <q-btn
                        class="!bg-green-500 text-white mr-1"
                        @click="updateProductClickHandler(props.row)"
                      >
                        Accept Order
                      </q-btn>
                    </div>
                  </template>
                  <template v-else-if="col.name=='Status'">
                    {{ col.value }}
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
import { Ref, computed, onMounted, ref } from 'vue';
import type { IProduct } from 'src/interface/ProductInterface';
import _ from 'lodash';
import AddProduct from 'subcomponent/AddProduct.vue';
import productRepository from 'api/productRepository';
import typeRepository from 'api/typeRepository';
import cartRepository from 'src/api/repository/cartRepository';


const showTable = ref(true);
const showAddProduct = ref(false);
const isUpdate = ref(false);
const modifiedProduct: Ref<IProduct> = ref();
const productType = ref([]);

let accountContain = [];
const addProductClickHandler = () => {
  isUpdate.value = false;
  showAddProduct.value = true;
  showTable.value = false;
}


const isAccountContain = (accountid) =>{
  const result = accountContain.includes(accountid);
  if(result){
    return false;
  }
  else{
    accountContain.push(accountid);
    return true;
  }
  
}
const updateProductClickHandler = async (product) => {
  await cartRepository.updateState(product.id,'Finished',true);
  accountContain  = [];
  getCart()
}

const backToListHandler = () => {
  showAddProduct.value = false;
  showTable.value = true;
  getCart();
}

const imageToLink = (images) => {
  if (images) {
    return `https://localhost:7082/${images.split(",")[0].trim()}`;
  }
}

const data = ref([]);

const columns = [
  { name: 'image', align: 'center', label: 'Image', field: row=>row.product.images, sortable: true },
  { name: 'name', label: 'Name', field: row=>row.product.name, sortable: true, align: 'left' , classes:'!w-[100px]'},
  { name: 'Quantity', label: 'Quantity', field: 'mount', align: 'left' },
  { name: 'To Price', label: 'Price', field: row=>"$" + row.mount*row.product.price , align: 'left',style:"color:red" },
  { name: '', label: 'Promote', field: row=>'none', align: 'left' },
  { name: 'Status', label: 'Status', field: row=>row.status, sortable: true, align: 'left', sort: (a, b) => parseInt(a, 10) - parseInt(b, 10) },
  // { name: 'Order Date', label: 'Order Date', field: row=>row.modifiedDate, sortable: true, sort: (a, b) => parseInt(a, 10) - parseInt(b, 10), align: 'left' },
    // { name: 'Function-button', label:'Action' ,align:'center'}
] as any

const rows = computed(() => {
  const rowsValue = _.clone(data.value);
  // rowsValue.push({
  //   name: 'function-button'
  // })
  return rowsValue
})

const getCart = async () => {
  const result = await cartRepository.getByFinished();
  data.value = result.payload;
}

const deleteProduct = async (product) => {
  const result = await productRepository.delete(product.id);
  getCart();

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
  getCart();
}

const getAllType = async () => {
  const result = await typeRepository.get();
  productType.value = result.payload;
}

onMounted(() => {
  getCart();
  getAllType();
});
</script>

<style scoped></style>