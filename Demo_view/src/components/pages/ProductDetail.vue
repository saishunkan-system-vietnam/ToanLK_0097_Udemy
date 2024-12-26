<template>
  <div class="border-l ">
    <div class="flex flex-nowrap ">
      <div class="pr-4 flex-1">
        <q-carousel
          v-if="product.images"
          v-model="imageSlide"
          v-model:fullscreen="fullscreen"
          swipeable
          animated
          thumbnails
          infinite
        >
          <q-carousel-slide
            v-for="(item, index) in product.images.split(',')"
            :key="index.toString()"
            :name="index"
            :img-src="`https://localhost:7082/${item}`"
          />

          <template #control>
            <q-carousel-control
              position="bottom-right"
              :offset="[18, 18]"
            >
              <q-btn
                push
                round
                dense
                color="white"
                text-color="primary"
                :icon="fullscreen ? 'fullscreen_exit' : 'fullscreen'"
                @click="fullscreen = !fullscreen"
              />
            </q-carousel-control>
          </template>
        </q-carousel>
      </div>
      <div class="flex-1">
        <div>Trang Chủ / Sản Phẩm</div>
        <div class="text-black text-2xl pb-2">
          {{ product["name"] }}
        </div>
        <div class="text-red-500 text-xl font-bold">
          {{ product["price"] }}₫
        </div>
        <div class="border-t-[1px] mt-4 text-black ">
          <span class="text-gray-400">Mã Sản Phẩm</span>: {{ product["code"] }}
        </div>
        <div class="border-t-[1px] mt-4 text-black">
          <span class="text-gray-400">Từ khóa</span>: {{ product["keyword"] }}
        </div>
        <div class="flex pt-10 justify-between">
          <QBtn
            class="!border-[#8071b3] !w-[212px] !py-4 !m-2"
            @click="addToCart"
          >
            Thêm vào giỏ hàng
          </QBtn>
          <QBtn
            class=" text-white !bg-[#8071b3] !w-[212px] !py-4 !m-2"
            @click="buyProduct"
          >
            Mua
          </QBtn>
          <div />
        </div>
      </div>
    </div>
    <div class="border-t mt-[48px]">
      <div class="w-[40%]">
        <q-tabs
          v-model="tab"
          narrow-indicator
          dense
          align="justify"
        >
          <q-tab
            class=""
            name="Description"
            label="Mô Tả"
          />
          <q-tab
            class=""
            name="Other"
            label="Thông Tin Khác"
          />
        </q-tabs>
      </div>
      <q-tab-panels
        v-model="tab"
        animated
      >
        <q-tab-panel
          name="Description"
          class="text-black"
        >
          <div v-html="product['description']" />
        </q-tab-panel>
        <q-tab-panel name="Other">
          <div class="text-black py-3 border-b flex justify-between">
            <div class="text-base flex-1">
              Bảo Hành
            </div>
            <div class="flex-1 text-left">
              {{ product["guarantee"] }} Tháng<span v-if="product['guarantee'] == 1">s</span>
            </div>
          </div>
        </q-tab-panel>
      </q-tab-panels>
    </div>
    <div class="ml-3">
      <div class="text-[20px] pt-4 pb-4">
        Sản Phẩm Liên Quan
      </div>
      <SlideShow
        v-if="relatedProductList.length != 0"
        v-model="relatedProductSlide"
        :items="relatedProductList"
        :mode="SLIDE_SHOW_MODE.CONTAIN_SLOT"
        height="400"
        :autoplay="4000"
        :arrows="true"
        :navigation="false"
        control-color="black"
        class="w-full"
      >
        <q-carousel-slide
          v-for="slideIndex in Math.ceilNumber(relatedProductList.length / 4)"
          :name="slideIndex"
          class="column no-wrap flex-center w-full h-full !p-0"
        >
          <div class="grid grid-cols-4 gap-1">
            <ProductItem
              v-for="productItem in 4"
              v-bind="relatedProductList[productItem + ((slideIndex - 1) * 4) - 1]"
            />
          </div>
        </q-carousel-slide>
      </SlideShow>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import productRepository from 'api/productRepository';
import { DIALOG_TYPE, SLIDE_SHOW_MODE } from 'src/common/enum';
import * as Math from 'common/math';
import SlideShow from 'components/base/SlideShow.vue';
import ProductItem from 'components/common/ProductItem.vue';
import cartRepository from 'api/cartRepository';
// eslint-disable-next-line vue/prefer-import-from-vue
import { inject } from '@vue/runtime-core'
import { useMainMenuStore } from 'src/stores/mainMenu';
import { useDialogStore } from 'src/stores/dialog';
const dialog = useDialogStore();



const mainMenuStore = useMainMenuStore();

const router = useRouter()
const route = useRoute();
const tab = ref("Description");
const fullscreen = ref(false);
const imageSlide = ref(0);
const relatedProductSlide = ref(1);
const relatedProductList = ref([]);
const product = ref({
  images: ""
});

const addToCart = async () => {
  const currentProductId = product.value["id"];
  const result = await cartRepository.addToCart(currentProductId, 1);
  if (!result.payload) {
    router.push("/login");
    return false
  }
  else{
    await dialog.show({
        type:DIALOG_TYPE.MESSAGE,
        header:'Thành Công',
        message:"Sản Phẩm đã được thêm vào giỏ hàng",
      });
  }
  mainMenuStore.myCallback();
  return true;
}

const getProduct = async (id) => {
  const result = await productRepository.getById(id);
  product.value = result.payload;
}

const getRelatedProduct = async (id) => {
  const result = await productRepository.getByType(id,8,1);
  relatedProductList.value = result.payload.product;
  relatedProductList.value = relatedProductList.value.slice(0,8);
}

const buyProduct = async () => {
  let isComplete = await addToCart();
  if(isComplete){
    router.push("/cart");
  }
}

onMounted(async () => {
  const id = router.currentRoute.value.params.id;
  await getProduct(id);
  getRelatedProduct(product.value["typeId"]);

});

watch(() => route.params.id, async () => {
  const id = router.currentRoute.value.params.id;
  getProduct(id);
  await getRelatedProduct(product.value["typeId"])
});

</script>

<style scoped>
.wrapper {
  width: 100%;
}

.block-product {
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
  padding-top: 24px;

  color: var(--main-color);
}
</style>