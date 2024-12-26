<template>
  <div class="wrapper select-none">
    <div class="text-xl pb-4">
      Thanh Toán
    </div>
    <div class="">
      <div class="grid grid-cols-10 gap-2 h-[60px] bg-slate-200 border">
        <div class="col-span-1 col-center h-[inherit]">
          Ảnh
        </div>
        <div class="col-span-4 col-center justify-center">
          Tên Sản Phẩm
        </div>
        <div class="col-span-1 col-center">
          Giá Sản Phẩm
        </div>
        <div class="col-span-2 col-center">
          <div>Số Lượng Sản Phẩm</div>
        </div>
        <div class="col-span-1 col-center">
          Tổng
        </div>
      </div>
      <template 
        v-for="item in productList"
        :key="item.id"
      >
        <div
          v-if="item.selected"
          class="grid grid-cols-10 gap-2 h-[100px] border border-t-0 text-black"
        >
          <div
            class="col-span-1 col-center "
            :class="[{ 'opacity-60': !item.selected }]"
          >
            <img
              :src="imageToLink(item.product.images)"
              alt=""
              class="w-[100px] h-[100px] p-1 object-cover"
            >
          </div>
          <div
            class="col-span-4 col-center !justify-start"
            :class="[{ 'opacity-60': !item.selected }]"
          >
            {{ item.product.name }}
          </div>
          <div
            class="col-span-1 col-center text-red-500 font-semibold"
            :class="[{ '!text-gray-400': !item.selected }]"
          >
            {{ item.product.price }} ₫
          </div>
          <div class="col-span-2 col-center">
            <div class="">
              <div
                class="flex"
                :class="[{ 'disabled': !item.selected }]"
              >
                <div
                  class="flex items-center justify-center w-8 h-8 bg-slate-200 opacity-50"
                >
                  <CIcon
                    icon="Remove"
                    class="!text-lg"
                  />
                </div>

                <div class="flex justify-center items-center w-8 h-8 border">
                  <span>
                    {{ item.mount }}
                  </span>
                </div>
                <div
                  class="flex items-center justify-center w-8 h-8 bg-slate-200 opacity-50"
                >
                  <CIcon
                    icon="Add"
                    class="!text-lg"
                  />
                </div>
              </div>
              <span class="text-xs mt-1 block">{{ item.product.mounts }} left in container</span>
            </div>
          </div>
          <div
            class="col-span-1 col-center text-red-500 font-semibold"
            :class="[{ '!text-gray-400': !item.selected }]"
          >
            {{ item.mount * item.product.price }} ₫
          </div>
        </div>
      </template>
    </div>
    <div class="mt-8 flex">
      <!-- TODO : coupon feature -->
      <div v-if="false">
        <div class="pb-1 pl-1">
          you saved {{ totalSavedCouponPrice }}₫
        </div>
        <div class="flex !flex-nowrap">
          <q-input
            v-model="coupon"
            outlined
            dense
            placeholder="Enter coupon code"
            class="w-full"
          />
          <q-btn class="text-black ml-3">
            Apply
          </q-btn>
        </div>
        <div class="w-full  pt-4">
          <div class="border flex  p-2">
            <div class="!flex-nowrap">
              Coupon Code: <span class="text-amber-400">{{ recentCouponCode }}</span> has been applied
            </div>
            <div class=" text-white bg-slate-400 border rounded-full w-5 h-5 flex justify-center items-center ml-3">
              <CIcon
                icon="Remove"
                class="ml-3"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="text-2xl w-fit ml-auto flex flex-col justify-between">
        <div class="ml-auto">
          <span>Total</span>:
          <span>₫{{ totalPrice }}</span>
        </div>
        <div class="pt-auto">
          <!-- <q-btn
            class="mr-4"
            @click="backToShop"
          >
            Continue Shopping
          </q-btn> -->
          <q-btn
            class="!bg-[#8071b3] text-white"
            @click="purchase"
          >
            Order
          </q-btn>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import cartRepository from 'api/cartRepository';
import { useDialogStore } from 'src/stores/dialog';
const productList = ref([]);
const coupon = ref("");
const totalSavedCouponPrice = ref(0);
const recentCouponCode = ref('D2SDA');
import { DIALOG_TYPE } from 'src/common/enum';
import {useRouter} from 'vue-router';
const dialog = useDialogStore();
const router = useRouter();

const props = defineProps([
  'orderProducts'
]);
const selectedProduct = ref([]);
const getCartProduct = async () => {
  const result = await cartRepository.getProductByAccount();
  // not login yet
  if(result.payload == null){
    const value = await dialog.show({
        type:DIALOG_TYPE.MESSAGE,
        header:'Lỗi',
        message:"Bạn phải đăng nhập trước.",
      },()=>{router.push('/login')});
  }
  productList.value = result.payload;
}

const totalPrice = computed(() => {
  let totalValue = 0;
  if(productList.value && productList.value.length != 0){
    for(let item of productList.value){
      if(item.selected){
        totalValue += (item.product.price * item.mount);
      }
    }
  }
  return totalValue;
})

const imageToLink = (images) => {
  if (images) {
    return `https://localhost:7082/${images.split(",")[0].trim()}`;
  }
}

const up = async (id) => {
  await cartRepository.updateMountProduct(id, 1);
  getCartProduct();
}

const down = async (id) => {
  await cartRepository.updateMountProduct(id, -1);
  getCartProduct();
}

const changeSelectCart = (cartId) =>{
  cartRepository.changeSelected(cartId);
}

const purchase = async () => {
  for (var cart of productList.value) {
    if(cart.selected){
      await cartRepository.updateState(cart.id, "InOrder", true);
    }
  }
  getCartProduct();
}

const deleteItem = async (cartId) =>{
  await cartRepository.deleteCart(cartId);
  getCartProduct();
}

onMounted(() => {
  getCartProduct();

})

</script>

<style scoped>
.wrapper {
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
  padding-top: 24px;

  color: var(--main-color);
  /* @apply  */
}

.col-center {
  @apply flex justify-center items-center
}

.delete-text{
  @apply font-light text-sm;

}

.delete-text:hover{
  @apply text-[var(--main-color)] cursor-pointer;
}
</style>