<template>
  <MainMenu
    v-if="route.currentRoute.value.meta.showMainMenu"
    :current-user="currentUser"
  />
  <RouterView />
  <!-- <SignalRExpose /> -->
</template>
<script setup lang="ts">
import AdminPage from 'src/components/pages/AdminPage.vue';
import MainMenu from 'views/pages/MainMenu.vue';
import SideMenu from 'views/pages/SideMenu.vue';
import SignalRExpose from 'src/components/tools/SignalRExpose.vue';
import accountRepository from 'repository/accountRepository';
import { RouterView } from 'vue-router';
import { useRouter } from 'vue-router';
import {onMounted, ref, provide } from 'vue';
import { useMainMenuStore } from 'stores/mainMenu';

const route = useRouter();
const currentUser = ref();

const  mainMenuStore = useMainMenuStore();
//get me


const getMe = async() =>{
  let getMeResult = await accountRepository.getMe();
  if(getMeResult.payload !== null){
    currentUser.value = getMeResult.payload;
  }
}

mainMenuStore.getMe = getMe;

provide('getMe',getMe);

onMounted(async()=>{
  getMe();
})
</script>
<style scoped></style>