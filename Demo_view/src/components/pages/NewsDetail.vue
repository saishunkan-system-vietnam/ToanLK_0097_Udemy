<template>
  <div class="wrapper">
    <div class="w-[240px]">
      <SideMenu />
    </div>
    <div
      class="px-4 mt-4 shadow-md"
    >
      <div>
        <div class="entry-header-text entry-header-text-top text-left">
          <h6 class="entry-category text-[12px]">
            <a
              href="https://akkogear.com.vn/tin-tuc/"
              rel="category tag"
            >Tin Tức</a>
          </h6>
          <h1 class="entry-title text-[24px] leading-7 text-black">
            {{ currentNew.title }}
          </h1>
          <div class="entry-divider is-divider small" />
        </div>
      </div>
      <div v-html="currentNew.content" />
    </div>
  </div>
  <FooterInfo />
</template>

<script setup lang="ts">
import SideMenu from 'views/pages/SideMenu.vue';
import FooterInfo from 'components/subpage/FooterInfo.vue';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import newsRepository from 'repository/newsRepository';
const router = useRoute();
const currentNewsId = ref();
const currentNew = ref({});

onMounted(async () => {
  currentNewsId.value = router.params.id;
  let result = await newsRepository.getById(currentNewsId.value);
  currentNew.value = result.payload;

})

</script>

<style scoped>
.wrapper {
  display: flex;
  justify-content: space-between;
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
  padding-top: 24px;
  gap: 24px;
  color: var(--main-color);
  /* @apply  */
}

.article-inner.has-shadow .entry-header-text-top {
  padding-top: 1.5em;
}

a {
    color: #334862;
    text-decoration: none;
}
.is-divider {
    height: 3px;
    display: block;
    background-color: rgba(0,0,0,.1);
    margin: 1em 0 1em;
    width: 100%;
    max-width: 30px;
}
</style>