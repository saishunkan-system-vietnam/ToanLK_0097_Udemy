<template>
  <div class="max-w-[1280px] mx-[auto] px-2">
    <div class="pt-4">
      <h6>Thay Đổi Mật khẩu</h6>

      <div class="pb-3 pt-3"> 
        <q-input
          v-model="user.currentPassword"
          outlined
          label="Mật Khẩu Hiện Tại"
          dense
        />
      </div>

      <div class="pb-3 pt-3"> 
        <q-input
          v-model="user.password"
          outlined
          label="Mật Khẩu"
          dense
        />
      </div>
      <div class="pb-3">
        <q-input
          v-model="user.confirmPassword"
          outlined
          label="Nhập Lại Mật Khẩu"
          dense
          :rule="[
            val=>val == '123' || 'Mật khẩu không khớp.'
          ]"
        />
      </div>
      <div class="ml-auto w-fit">
        <q-btn
          color="primary"
          label="Thay đổi mật khẩu"
          class=""
          @click="changePasswordClickHandler()"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import {ref} from 'vue';
  import accountRepository from 'api/accountRepository';
  import { useDialogStore } from 'stores/dialog';
  import { useCookies } from 'vue3-cookies';

  import { DIALOG_TYPE } from 'common/enum';
  import { useRouter } from 'vue-router';
  const { cookies } = useCookies();

  const router = useRouter()
  const useDialog = useDialogStore();
  const user = ref({
    currentPassword:'',
    password:'',
    confirmPassword:'',
  })
  const changePasswordClickHandler = async() =>{
    console.log(user.value);
    const result = await accountRepository.changePassword(
      {
        currentPassword:user.value.currentPassword,
        password:user.value.password
      });
    if(!result.payload){
      useDialog.show({
      type:DIALOG_TYPE.MESSAGE,
      header:'Lỗi',
      message:"Thay đổi mật khẩu không thành công. hãy kiểm tra lại"});
      return;
    }
      useDialog.show({
      type:DIALOG_TYPE.MESSAGE,
      header:'Thành Công',
      message:"Mật khẩu của bạn đã được thay đổi thành công.Hãy đăng nhập lại",
    },()=>{
      cookies.remove("token");
      router.push("/login");
    })
  }
</script>

<style scoped>

</style>