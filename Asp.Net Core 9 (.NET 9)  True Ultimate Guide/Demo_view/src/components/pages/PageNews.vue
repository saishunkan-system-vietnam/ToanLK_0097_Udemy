<template>
  <div class="wrapper">
    <div class="mt-4">
      <article
        v-for="newsPost in newsList"
        :key="newsPost"
        class="post-49641 post type-post status-publish format-standard has-post-thumbnail hentry category-tin-tuc tag-akko-mach-xuoi tag-akko-mach-xuoi-south-facing-chinh-thuc-ra-mat"
      >
        <div class="article-inner has-shadow box-shadow-1 box-shadow-2-hover w-full">
          <header class="entry-header">
            <div class="entry-header-text text-left">
              <h6 class="entry-category is-xsmall ">
                <a
                  href=""
                  rel="category tag"
                >News</a>
              </h6><h2 class="entry-title">
                <a
                  href=""
                  rel="bookmark"
                  class="plain text-[20px]"
                >{{ newsPost.title }}</a>
              </h2><div class="entry-divider is-divider small" /><div class="entry-meta uppercase is-xsmall">
                <span class="posted-on">Posted on <a
                  href=""
                  rel="bookmark"
                ><time
                  class="entry-date published"
                  datetime="2022-08-17T08:46:17+07:00"
                >{{ 
                  formatDate(newsPost.createDate).formattedDate }}</time>
                </a></span><span class="byline"> by <span class="meta-author vcard"><a
                  class="url fn n"
                  href="author/admin/"
                >AKKO Gear</a></span></span>
              </div>
            </div>
          </header>
          <div class="entry-image-float">
            <a :href=" 'news-detail/' + newsPost.id"> <img
              width="420"
              height="226"
              :src="imageToLink(newsPost.images)"
              class="attachment-large size-large wp-post-image flex-1 !h-[224px] object-cover"
              alt="akko-mach-xuoi-ra-mat-ava"
              loading="lazy"
            ></a><div class="badge absolute top post-date badge-square">
              <div class="badge-inner">
                <span class="post-date-day">
                  {{ formatDate(newsPost.createDate).date }}
                </span><br> <span class="post-date-month is-small">Th{{ formatDate(newsPost.createDate).month }}</span>
              </div>
            </div>
            <div class="entry-content flex-1">
              <div class="entry-summary">
                <p>
                  <span
                    class="h-[120px] block overflow-hidden"
                  >
                    {{ compactText(newsPost.content) }}
                  </span>
                </p>
                <div class="text-left">
                  <a
                    class="more-link button primary is-outline is-smaller"
                    :href=" 'news-detail/' + newsPost.id"
                  >Tiếp tục đọc <span class="meta-nav">→</span></a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </article>
    </div>
    <div>
      <SideMenu />
    </div>
  </div>
  <FooterInfo />
</template>

<script setup lang="ts">
import SideMenu from 'views/pages/SideMenu.vue';
import FooterInfo from 'components/subpage/FooterInfo.vue';
import { onMounted, ref } from 'vue';
import newsRepository from 'repository/newsRepository';
const newsList = ref([]);

const getNewsList = async () =>{
  let result = await newsRepository.get();
  newsList.value = result.payload;
}

const formatDate = (date) =>{
  const currentDate = new Date(date);
  const formattedDate = currentDate.getDate() + "/" + (currentDate.getMonth() + 1)  + "/" + currentDate.getFullYear();
  return {formattedDate,date:currentDate.getDate(), month:(currentDate.getMonth()+1) };
}

onMounted(async()=>{
  await getNewsList();
})

const compactText = (text) =>{
  let plainText = hyperTextToNormal(text);
  let iter = 300;
  let compactText = plainText.substring(0,iter);
  let result =  compactText + "...";
  return result;
}

const hyperTextToNormal = (hyperText)=>{
  let plainText = hyperText.replace(/<[^>]*>/g, '');
  return plainText;
}


const imageToLink = (images) => {
  if (images) {
    return `https://localhost:7082/${images.split(",")[0].trim()}`;
  }
}


</script>

<style scoped>
h2{
  font-size:inherit;
  font-weight: inherit;
  line-height: inherit;
}

.wrapper {
  display:flex;
  justify-content: space-between;
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
  padding-top: 24px;
  gap:24px;
  color: var(--main-color);
  /* @apply  */
}
  .post {
    margin: 0 0 30px;
}
.article-inner.has-shadow {
  box-shadow: 0 1px 3px -2px rgba(0,0,0,.12), 0 1px 2px rgba(0,0,0,.24);
    background-color: #fff;
}
.article-inner {
    transition: opacity .3s,box-shadow .5s,transform .3s;
}
.article-inner.has-shadow .entry-content, .article-inner.has-shadow footer.entry-meta, .article-inner.has-shadow .entry-header-text, .article-inner.has-shadow .author-box {
    padding-left: 1.5em;
    padding-right: 1.5em;
}

.entry-header-text {
    padding: 1.5em 0 1.5em;
}

.text-left {
    text-align: left;
}

.is-xsmall {
    font-size: .7em;
}
a {
    color: #334862;
    text-decoration: none;
}
a.plain {
    color: currentColor;
    transition: color .3s,opacity .3s,transform .3s;
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
.is-xsmall {
    font-size: .7em;
}
.uppercase {
    line-height: 1.2;
    text-transform: uppercase;
}
.entry-image-float {
    margin-right: 2em;
    display: flex;
    flex:1;
}
.entry-image-float {
    position: relative;
}
img {
    transition: opacity 1s;
    opacity: 1;
}

img {
    max-width: 100%;
    height: auto;
    display: inline-block;
    vertical-align: middle;
}
.badge.post-date {
    top: 7%;
}
.badge.top {
  left: 0;
}
.top {
    top: 0;
}
.absolute {
    position: absolute!important;
}
.badge {
    display: table;
    z-index: 20;
    pointer-events: none;
    height: 2.8em;
    width: 2.8em;
    -webkit-backface-visibility: hidden;
    backface-visibility: hidden;
}

.badge-inner {
    background-color: #8071b3;
}


.badge-inner {
    display: table-cell;
    vertical-align: middle;
    text-align: center;
    width: 100%;
    height: 100%;
    background-color: #446084;
    line-height: .85;
    color: #fff;
    font-weight: bolder;
    padding: 2px;
    white-space: nowrap;
    transition: background-color .3s,color .3s,border .3s;
}

.entry-image-float+.entry-content {
    padding-top: 0;
}
.entry-content {
    padding-top: 1.5em;
    padding-bottom: 1.5em;
}
.post .entry-summary p:last-of-type {
    margin-bottom: 0;
}

.is-outline.primary {
    color: #8071b3;
}

.primary.is-underline, .primary.is-link, .primary.is-outline {
    color: #446084;
}

footer.entry-meta {
    font-size: .8em;
    border-top: 1px solid #ececec;
    border-bottom: 2px solid #ececec;
    padding: 0.5em 0 0.6em;
}
</style>