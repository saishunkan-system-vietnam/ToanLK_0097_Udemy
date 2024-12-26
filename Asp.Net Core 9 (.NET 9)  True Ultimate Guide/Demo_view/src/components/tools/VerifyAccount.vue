<template>
  <div class="">
    <div
      v-if="isSuccess"
      class="wrapper"
    >
      <svg
        class="checkmark"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 52 52"
      > <circle
        class="checkmark__circle"
        cx="26"
        cy="26"
        r="25"
        fill="none"
      /> <path
        class="checkmark__check"
        fill="none"
        d="M14.1 27.2l7.1 7.2 16.7-16.8"
      />
      </svg>
    </div>
    <div
      v-if="isError"
      class="wrapper1"
    >
      <svg
        class="checkmark"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 52 52"
      ><circle
        class="checkmark_circle"
        cx="26"
        cy="26"
        r="25"
        fill="none"
      /><path
        class="checkmark_check"
        fill="none"
        d="M14.1 14.1l23.8 23.8 m0,-23.8 l-23.8,23.8"
      /></svg>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from 'vue';
  import  AccountRepository from "repository/accountRepository";
  import { useRoute } from 'vue-router'
import { STATUS_CODE } from 'src/common/StatusCode';
import { useDialogStore } from 'src/stores/dialog';
import { DIALOG_TYPE } from 'src/common/enum';

const isSuccess = ref(false);
const isError = ref(false);
const dialogStore = useDialogStore();
  const route = useRoute()
  onMounted(async()=>{
    const result = await AccountRepository.verifyAccount(route.params.id);
    if(result.code == STATUS_CODE.BAD_REQUEST){
      isError.value = true;
      dialogStore.show({
        type:DIALOG_TYPE.MESSAGE,
        header:"Error",
        message:"It ran out of time. please sign up again"
      });
    }else{
      isError.value = false;
      isSuccess.value = true;
      dialogStore.show({
        type:DIALOG_TYPE.MESSAGE,
        header:"Success",
        message:"Verify account completely succeed"
      });
    }
  })
</script>

<style scoped>
*{padding:0;margin:0}.wrapper{height:100vh;display:flex;justify-content:center;align-items:center;background-color:#eee}.checkmark__circle{stroke-dasharray: 166;stroke-dashoffset: 166;stroke-width: 2;stroke-miterlimit: 10;stroke: #7ac142;fill: none;animation: stroke 0.6s cubic-bezier(0.65, 0, 0.45, 1) forwards}.checkmark{width: 56px;height: 56px;border-radius: 50%;display: block;stroke-width: 2;stroke: #fff;stroke-miterlimit: 10;margin: 10% auto;box-shadow: inset 0px 0px 0px #7ac142;animation: fill .4s ease-in-out .4s forwards, scale .3s ease-in-out .9s both}.checkmark__check{transform-origin: 50% 50%;stroke-dasharray: 48;stroke-dashoffset: 48;animation: stroke 0.3s cubic-bezier(0.65, 0, 0.45, 1) 0.8s forwards}@keyframes stroke{100%{stroke-dashoffset: 0}}@keyframes scale{0%, 100%{transform: none}50%{transform: scale3d(1.1, 1.1, 1)}}@keyframes fill{100%{box-shadow: inset 0px 0px 0px 30px #7ac142}}

*{
    padding:0;
    margin:0;
}

body {
  width: 100vw;
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color:#eee;
}

.checkmark {
  width: 56px;
  height: 56px;
  border-radius: 50%;
  display: block;
  stroke-width: 2;
  stroke: #fff;
  stroke-miterlimit: 10;
  box-shadow: inset 0px 0px 0px #7ac142;
  animation: fill .4s ease-in-out .4s forwards, scale .3s ease-in-out .9s both;
}

.checkmark_circle {
  stroke-dasharray: 166;
  stroke-dashoffset: 166;
  stroke-width: 2;
  stroke-miterlimit: 10;
  stroke: #7ac142;
  fill: none;
  animation: stroke 0.6s cubic-bezier(0.65, 0, 0.45, 1) forwards;
}

.checkmark_check {
  transform-origin: 50% 50%;
  stroke-dasharray: 48;
  stroke-dashoffset: 48;
  animation: stroke 0.3s cubic-bezier(0.65, 0, 0.45, 1) 0.8s forwards;
}

@keyframes stroke {
  100% {
    stroke-dashoffset: 0;
  }
}
@keyframes scale {
  0%, 100% {
    transform: none;
  }
  50% {
    transform: scale3d(1.1, 1.1, 1);
  }
}
@keyframes fill {
  100% {
    box-shadow: inset 0px 0px 0px 30px #7ac142;
  }
}
</style>