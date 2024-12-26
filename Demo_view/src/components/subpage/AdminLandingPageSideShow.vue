<template>
  <div class="wrapper">
    <div>
      <div class="py-2 text-lg flex">
        Timelife 
        <q-btn class="ml-10 block ">
          Add Image
        </q-btn>
        <q-btn class="block ml-[auto]">
          Save
        </q-btn>
      </div>
      <div class="flex ml-[-.5rem]">
        <div
          v-for="(image,index) in imageList"
          :key="index"
          class=" m-2 "
        >
          <div class="image-item relative">
            <img
              :src="image.src"
              alt=""
              class="w-[200px] h-auto rounded-lg"
            >
            <div
              class="hidden absolute top-0 left-0 right-0 bottom-0 bg-[#0303038a] rounded-lg"
              rounded-lg
            >
              <button class="  bg-red-500 text-white w-[40px] h-[40px] rounded-full absolute top-[-10px] right-[-10px]">
                ✕
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div>
      <div class="py-2 text-lg">
        Preview
      </div>
      <SlideShow
        v-model="slideIndex"
        :items="imageList"
        :autoplay="false"
        class="!h-[50vh] !w-full"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import SlideShow from 'base/SlideShow.vue';
const slideIndex = ref(0)
  
  const imageList = ref([
    {
      src:'https://dictionary.cambridge.org/rss/images/power-skills.jpg'
    },
    {
      src:"/images/landingPage/5075B-Plus-goku-naruto-01.jpg"
    },
  ])
</script>

<style scoped>
.image-item{
  @apply transition-all
}
.image-item:hover > div {
  display: flex !important;
}
</style>