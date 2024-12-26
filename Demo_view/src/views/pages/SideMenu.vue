<template>
  <div class="w-[240px] mr-5">
    <div>
      <div class="p-2 bg-[#8071b3] text-white w-full text-lg mt-4">
        Tin Tức
      </div>
      <template
        v-for="(newsPost,index) in newestPostNews"
        :key="news"
      >
        <a
          class="flex !flex-nowrap border border-t-0"
          href=""
        >
          <div class="flex items-center">
            <img
              :src="imageToLink(newsPost.images)"
              class="!w-[70px] !h-[70px] !max-w-[unset] object-cover"
            >
          </div>
          <div class="pl-3 py-3 pr-2">
            <div
              class=" text-black h-[64px]"
            >
              {{ newsPost.title }}
            </div>
          </div>
        </a>
      </template>
    </div>

    <div>
      <div class="p-2 bg-[#8071b3] text-white w-full text-lg mt-4 ">
        Sản Phẩm Mới
      </div>
      <template
        v-for="(product) in sellingProducts"
        :key="product"
      >
        <div class="flex !flex-nowrap border border-t-0 ">
          <div class="flex items-center">
            <img
              :src="imageToLink(product.images)"
              class="!w-[70px] !h-[70px] !max-w-[unset] object-cover"
            >
          </div>
          <div class="pl-3  py-2 pr-2">
            <div class=" text-black ">
              {{ product.name }}
            </div>
            <div class="text-red-500 font-bold">
              {{ product.price }} $
            </div>
          </div>
        </div>
      </template>
    </div>
  </div>
</template>


<script setup lang="ts">
import { onMounted, ref } from 'vue';
import newsRepository from 'repository/newsRepository';
import productRepository from 'repository/productRepository';

const newestPostNews = ref([]);

const sellingProducts = ref([]);

onMounted(async()=>{
  const result = await newsRepository.getByQuantity(3);
  newestPostNews.value = result.payload;

  const productResult = await productRepository.getByQuantity(3);
  sellingProducts.value = productResult.payload;

})

const imageToLink = (images) => {
  if (images) {
    return `https://localhost:7082/${images.split(",")[0].trim()}`;
  }
}

</script>
