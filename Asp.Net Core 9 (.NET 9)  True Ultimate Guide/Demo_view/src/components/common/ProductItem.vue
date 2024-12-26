<template>
  <div
    class="h-[390px]"
    v-bind="$attrs"
    :class="props.class"
    @click="goToProductDetail"
  >
    <div class="border h-full flex flex-col flex-auto !flex-nowrap">
      <div class="product-image w-[100%] flex items-center justify-center overflow-hidden  flex-auto">
        <img
          :src="presentImageProduct"
          :alt="props.alt"
          class="w-[100%] h-[100%] product-img object-cover"
        >
      </div>
      <div class="product-info flex-none">
        <div class="text-center h-[64px]">
          {{ props.name }}
        </div>
        <div class="text-center text-red-500 font-bold pb-[1px]">
          {{ props.price }} USD
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang='ts'>
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
const props = defineProps({
  images: {
    required: true,
    type: String,
    default: ""
  },
  alt: {
    required: true,
    type: String,
    default: ''
  },
  name: {
    required: true,
    type: String,
    default: 'error text'
  },
  price: {
    required: false,
    type: String,
    default: '00.00'
  },
  class: {
    require: false,
    type: String
  },
  id: {
    type: String,
    required: true,
    default: ""
  }
});
const router = useRouter();
const productImage = ref();
const presentImageProduct = computed(()=>{
  let imageList = props.images.split(",")
  if (imageList.length == 0) {
    return "images/logo/10.png";
  } else {
    return `https://localhost:7082/${imageList[0]}`;
  }
});
onMounted(() => {
  // let imageList = props.images.split(",")
  // if (imageList.length == 0) {
  //   presentImageProduct.value = "images/logo/10.png";
  // } else {
  //   presentImageProduct.value = `https://localhost:7082/${imageList[0]}`;
  // }
})

const goToProductDetail = () => {
  router.push({ path: `/product/detail/${props.id}` })
}
</script>
<style scoped lang='scss'>
.product-img {
  transition: transform .6s;
}

.product-img:hover {
  transform: scale(1.1);
}

.product-info {
  @apply h-[90px]
}


</style>