<template>
  <div class="wrapper">
    <div class="main-container">
      <div class="flex justify-center h-auto">
        <a href="/">
          <img
            src="/images/logo/logo-akkogear.png"
            alt="logo image"
            class=""
          >
        </a>
      </div>
      <div class="pl-4">
        <template
          v-for="route in routes"
          :key="route.name"
        >
          <RouterLink
            v-if="route.meta.show"
            :to="route.path"
            class="button-link"
          >
            {{ route.name }}
          </RouterLink>
        </template>
        <div />
      </div>
      <div class="ml-auto">
        <div class="menu-icon">
          <CIcon
            icon="Shopping Cart"
            class="w-12 !text-xl text-center"
            @mouseenter="cartDropdown = true"
            @mouseleave="cartDropdown = false"
            @click="gotoCartDetail"
          />
        </div>
        <q-btn-dropdown
          v-model="cartDropdown"
          color="primary"
          class="dropdown overflow-visible"
        >
          <div class="">
            <div class="">
              <template v-if="productCart && productCart.length != 0">
                <div
                  v-for="product in productCart"
                  :key="product.toString()"
                  class="grid grid-cols-5 gap-1 max-w-[500px] p-2"
                >
                  <div>
                    <img
                      :src="imageToLink(product.product.images)"
                      alt=""
                      class="w-[80px] h-[80px] object-cover p-1"
                    >
                  </div>
                  <div class="col-span-3">
                    {{ product.product.name }}
                  </div>
                  <div class="col-span-1 text-right text-red-500">
                    {{ product.product.price }} USD
                  </div>
                </div>
              </template>
              <div
                v-else
                class="w-[400px] h-[80px] flex justify-center items-center"
              >
                there is no product!
              </div>
            </div>
          </div>
        </q-btn-dropdown>
        <div class="menu-icon">
          <div
            v-if="props.currentUser != null"
            class=""
            @click="getUserInformation()"
          >
            <img
              v-if="props.currentUser.avatar"
              class="avatar-icon"
              :src="props.currentUser.avatar"
              alt=""
            >
            <img
              v-else
              class="avatar-icon"
              src="/images/login/default-avatar.jpg"
              alt=""
            >
          </div>
          <div v-else>
            <CIcon
              icon="Person"
              class="w-12 !text-2xl text-center"
              @click="()=>{router.push('/login')}"
            />
          </div>
        </div>
        <q-btn-dropdown
          v-model="dropdown"
          color="primary"
          class="dropdown"
        >
          <q-list>
            <q-item
              v-close-popup
              clickable
              @click="informationClickHandler"
            >
              <q-item-section>
                <q-item-label>
                  <div class="flex">
                    <CIcon icon="Info" />
                    <span class="w-[120px] pl-2 ">
                      Information
                    </span>
                  </div>
                </q-item-label>
              </q-item-section>
            </q-item>

            <q-item
              v-close-popup
              clickable
              @click="changePasswordClickHandler()"
            >
              <q-item-section>
                <q-item-label>
                  <div class="flex">
                    <CIcon icon="Info" />
                    <span class="w-[120px] pl-2 ">
                      Đổi Password
                    </span>
                  </div>
                </q-item-label>
              </q-item-section>
            </q-item>

            <q-item
              v-close-popup
              clickable
              @click="signOutClickHandler"
            >
              <q-item-section>
                <q-item-label>
                  <div class="flex">
                    <CIcon icon="Logout" />
                    <span class="w-[120px] pl-2">
                      Sign Out
                    </span>
                  </div>
                </q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </q-btn-dropdown>
      </div>
      <div />
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, provide } from 'vue';

import { useRouter, RouterLink } from 'vue-router';
import { useDialogStore } from 'src/stores/dialog';
import { useCookies } from 'vue3-cookies';
import accountRepository from 'src/api/repository/accountRepository';
import cartRepository from 'src/api/repository/cartRepository';
import { useMainMenuStore } from 'src/stores/mainMenu';
const mainMenuStore = useMainMenuStore();
const router = useRouter();
const routes = router.getRoutes();
const dialog = useDialogStore();
const { cookies } = useCookies();

const props = defineProps({
  currentUser:{
    type:Object
  }
});

const cartDropdown = ref(false);
const toggleLogo = ref(false);
const dropdown = ref(false);
const informationClickHandler = () => {
  router.push('/user')
}
const productCart = ref([]);

const getProductCart = async () => {
  const result = await cartRepository.getProductByAccount();
  productCart.value = result.payload;
}

mainMenuStore.myCallback = getProductCart;

provide('get-product-cart-list', 'tes1');


const signOutClickHandler = () => {
  cookies.remove("token");
  router.push("/login");
}

const gotoCartDetail = () => {
  router.push("/cart");
}

const changePasswordClickHandler = () =>{
  console.log('test');
  router.push('/password');
}
 
onMounted(() => {
  setInterval(() => {
    toggleLogo.value = !toggleLogo.value
  }, 500);
  getProductCart();
})

const imageToLink = (images) => {
  if (images) {
    return `https://localhost:7082/${images.split(",")[0].trim()}`;
  }
}

const  getUserInformation = async () =>{
  dropdown.value = true;
}
</script>

<style scoped lang="scss">
.wrapper {
  z-index: 1;
  box-shadow: 0 1px 3px -3px rgb(0, 0, 0);
  @apply bg-white sticky top-0 left-0 right-0
}

.main-container {
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
  @apply flex;
}

.main-container {
  div {
    @apply flex items-center h-auto
  }
}

.button-link {
  @apply text-lg text-black h-full py-3 px-7 hover:bg-slate-300 transition-all flex items-center justify-center 
}

.menu-icon {
  @apply flex justify-center items-center cursor-pointer
}

.dropdown {
  width: 1px;
  height: 1px;
  padding: 0;
  margin: 0;
  visibility: hidden;
}

.avatar-icon{
  object-fit: cover;
  @apply w-[30px] h-[30px] rounded-full
}
</style>