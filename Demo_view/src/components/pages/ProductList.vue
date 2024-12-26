<template>
  <div class="flex-auto">
    <div class="w-full flex">
      <div class="py-4 min-w-[30px] mx-1">
        <q-select
          v-model="sortBy"
          :options="sortOptions"
          option-value="value"
          outlined
          label="sắp xếp"
          dense
          option-label="name"
          class="w-full"
        />
      </div>
    </div>
    <div class="products grid grid-cols-4 gap-1">
      <ProductItem
        v-for="(product, index) in productList"
        :key="index"
        v-bind="product"
        :test="product.createDate"
      />
    </div>
    <PagingProduct 
      v-model="currentPage"
      :quantity-of-pages="totalPage"
    />
  </div>
</template>
<script setup lang='ts'>
import SideMenu from 'views/pages/SideMenu.vue';
import ProductItem from 'components/common/ProductItem.vue';
import FooterInfo from 'subpage/FooterInfo.vue';
import { onMounted, ref, watch } from 'vue';
import PagingProduct from 'components/common/PagingProduct.vue';

const props = defineProps({
  BreadCrumb: {
    require: true,
    type: String,
    default: "TRANG CHỦ / BÀN PHÍM"
  }
});

const currentPage = ref(0);
const totalPage = ref(0)
const sortBy = ref();

const sortOptions = ref([
  {
    name:"Mới Nhất",value:"asc"
  }, 
  {
    name:"Cũ Nhất", value:'desc'
  }
]);
const optionOrder = [
  'newest to oldest', 'oldest to newest'
]

const orderBy = ref(optionOrder[0]);
const productList = ref([])
import {useRouter } from 'vue-router';
import productRepository from 'src/api/repository/productRepository';
const router = useRouter()

const getProduct = async(quantity=12,numOfPage=1) =>{
  const currentRouterName =  router.currentRoute.value.name;
  switch (currentRouterName) {
    case "keyboard":
      // eslint-disable-next-line no-case-declarations
      const keyboardresult =  await productRepository.getByType("9ece3d55-2635-48a0-a65b-4f1cd9af95a1",quantity,numOfPage);
      productList.value = keyboardresult.payload.product;
      totalPage.value = keyboardresult.payload.count;
      break;
    case "mouse":
      // eslint-disable-next-line no-case-declarations
      const mouseResult =  await productRepository.getByType("190c853b-892d-4955-83f3-6c662fd56c9b",12,1);
      productList.value = mouseResult.payload.product;
      totalPage.value  = mouseResult.payload.count;
      break;
    case "kit":
      // eslint-disable-next-line no-case-declarations
      const kitResult =  await productRepository.getByType("396b2216-9b12-4a17-8310-608ebcbb1faa",12,1);
      productList.value = kitResult.payload.product;
      totalPage.value = kitResult.payload.count;
      break;
    case "gear":
    // eslint-disable-next-line no-case-declarations
      const gearResult =  await productRepository.getByType("3c6f04c8-e3ab-4e1a-8233-ee37302a4437",12,1);
      productList.value = gearResult.payload.product;
      totalPage.value = gearResult.payload.count;
      break;
    case "switch":
    // eslint-disable-next-line no-case-declarations
      const switchResult =  await productRepository.getByType("7aa5445d-dbea-4f57-b9e2-19977e7b0b01",12,1);
      productList.value = switchResult.payload.product;
      totalPage.value = switchResult.payload.count;
      break;
    case "keycap":
      // eslint-disable-next-line no-case-declarations
      const keycapResult =  await productRepository.getByType("92280de8-864d-4ad1-9ab4-8c847b47800e",12,1);
      productList.value = keycapResult.payload.product;
      totalPage.value = keycapResult.payload.count;
      break;
    default:
      break;
  }
}

watch(() => router.currentRoute.value.name, () => {
  getProduct();
});

onMounted(async ()=>{
  await getProduct();
  sortBy.value = sortOptions.value[0];
})

watch(sortBy,(newVal,oldVal)=>{
  if(newVal.value == "asc" && productList.value){
    console.log(productList.value);
    productList.value.sort(function(a,b){
      return new Date(b.createDate) - new Date(a.createDate);
    });

  }
  else if(newVal.value == "desc" && productList.value){
    console.log("desc")
    productList.value.sort(function(a,b){
      return   new Date(a.createDate) - new Date(b.createDate) ;
    });
  }
})

watch(currentPage,(newVal,oldVal)=>{
  getProduct(12,newVal+1);
});
</script>
<style scoped lang='scss'>
.wrapper {
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
  padding-top: 24px;

  color: var(--main-color);
  /* @apply  */
}

.products {
  @apply w-full mt-[-.25rem]
}

.flex-container {
  flex: 1 auto;
}

</style>