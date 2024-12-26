<template>
  <div
    class="flex !flex-nowrap"
  >
    <div class="w-fit">
      <img
        src="/images/landingPage/akko-3098B-banner-ngang-01.jpg"
        alt=""
        class="half-screen-image"
      >
    </div>
    <div class="bg-[#f7f7f7] w-full flex justify-center items-center  relative">
      <div class="h-fit w-[40%]">
        <div v-if="!isLogin">
          <div
            :class="{'hidden':tabNumber!=0}"
            class="tab-transition"
          >
            <div class="text-[36px] font-bold text-center text-[#8071b3]">
              Sign In
            </div>
            <span class="text-gray-400 pb-12 block text-center">Already have an account? <button
              class="text-[#515151] underline cursor-pointer"
              @click="switchPage"
            >login</button></span>
            <div>
              <q-input
                v-model="user.email"
                outlined
                type="email"
                label="Email address"
                class="py-2"
                :rules="[ 
                  (val, rules) => rules.email(val) || 'Please enter a valid email address',
                  val=>val.length <= 50||'Please use maximum 50 characters' 
                ]"
              />
              <q-input
                v-model="user.password"
                outlined
                :type="isPasswordShow ? 'password' : 'text'"
                label="Password"
                class="py-2"
                :rules="[ 
                  val=>val.length <= 25||'Please use maximum 50 characters' 
                ]"
              >
                <template #append>
                  <q-icon
                    :name="isPasswordShow ? 'visibility_off' : 'visibility'"
                    class="cursor-pointer"
                    @click="isPasswordShow = !isPasswordShow"
                  />
                </template>
              </q-input>
            </div>
            <div class="my-4 ">
              <q-btn
                class="!bg-[#8071b3] text-white !w-full !py-3"
                label="Create"
                @click="validateLoginInfo"
              />
              <span class="text-gray-400 text-center block mt-1">By joining, you agree to the <span class="underline text-[#515151] cursor-pointer">Terms</span> and <span class="underline text-[#515151] cursor-pointer">Privacy Policy</span></span>
            </div>
          </div>
          <div
            :class="{'hidden':tabNumber!=1}"
            class="tab-transition"
          >
            <div class="absolute top-0 left-0 ml-3 mt-3">
              <CIcon
                icon="Arrow Back"
                class="w-24 !text-[30px] text-center text-[#8071b3]"
                @click="tabNumber--"
              />
            </div>
            <div class="text-[24px] font-bold text-center text-[#8071b3]">
              Complete the following steps to configure your profile
            </div>
            <div>
              <input
                ref="avatarInput"
                type="file"
                name="avatar"
                class="!invisible !h-[1px]"
                accept="image/*"
                @change="uploadAvatarFileHandler($event)"
              >
              <div class="flex justify-center items-center">
                <q-btn
                  round
                  @click="inputAvatarFileHandler"
                >
                  <q-avatar size="56px">
                    <img :src="user.avatar == ''? 'images/login/default-avatar.jpg' : user.avatar">
                  </q-avatar>
                </q-btn>
              </div>
            </div>
            <div class="mt-4">
              <q-input
                v-model="user.name"
                outlined
                label="Name"
                class="py-2"
                :rules="[val=>val.length > 5 || 'make sure that your name is not shorter 5 characters']"
              />
              <q-input
                v-model="user.location"
                outlined
                label="Address"
                class="py-2"
                :rules="[val=>val.length > 5 || 'make sure that your location is not shorter 5 characters']"
              />
              <q-input
                v-model="user.phone"
                outlined
                type="number"
                label="Phone Number"
                class="py-2"
                :rules="[val=>validatePhoneNumber(val) || 'invalid phone number, please type again']"
              />
            </div>
            <q-btn
              class="!bg-[#8071b3] text-white !w-full !py-3"
              label="Continue"
              @click="createAccountClickHandler"
            />
          </div>
        </div>
        <div v-else>
          <div
            class="tab-transition"
          >
            <div class="text-[36px] font-bold text-center text-[#8071b3]">
              Log In
            </div>
            <span class="text-gray-400 pb-12 block text-center">don't have an account? <button
              class="text-[#515151] underline cursor-pointer"
              @click="switchPage"
            >sign up</button></span>
            <div>
              <q-input
                v-model="user.email"
                outlined
                type="email"
                label="Email address"
                class="py-2"
                :rules="[ 
                  (val, rules) => rules.email(val) || 'Please enter a valid email address',
                  val=>val.length <= 50||'Please use maximum 50 characters' 
                ]"
              />
              <q-input
                v-model="user.password"
                outlined
                :type="isPasswordShow ? 'password' : 'text'"
                label="Password"
                class="py-2"
                :rules="[ 
                  val=>val.length <= 25||'Please use maximum 50 characters' 
                ]"
              >
                <template #append>
                  <q-icon
                    :name="isPasswordShow ? 'visibility_off' : 'visibility'"
                    class="cursor-pointer"
                    @click="isPasswordShow = !isPasswordShow"
                  />
                </template>
              </q-input>
            </div>
            <div class="my-4 ">
              <q-btn
                class="!bg-[#8071b3] text-white !w-full !py-3"
                label="Login"
                @click="loginClickHandler"
              />
              <span class="text-gray-400 text-center block mt-1">By joining, you agree to the <span class="underline text-[#515151] cursor-pointer">Terms</span> and <span class="underline text-[#515151] cursor-pointer">Privacy Policy</span></span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { inject, onMounted, ref } from 'vue';
  import accountRepository from 'repository/accountRepository';
  import { STATUS_CODE } from 'src/common/StatusCode';
  import {useDialogStore} from 'stores/dialog';
  import { DIALOG_TYPE } from 'src/common/enum';
  import { useRoute, useRouter } from 'vue-router';
  import { useCookies } from "vue3-cookies";
  import { useMainMenuStore } from 'stores/mainMenu';

  const route = useRoute();
  const router = useRouter();
  const dialog = useDialogStore();
  const {cookies} = useCookies();
  const mainMenuStore = useMainMenuStore();

  const isLogin = ref(false);
  const tabNumber = ref(0);
  const isPasswordShow = ref(true);
  const user =  ref({
    name:"",
    email:"",
    password:"",
    phone:"",
    location:"",
    avatar:""
  });
  

  const avatarInput = ref();

  const switchPage = () =>{
    isLogin.value = !isLogin.value;
    user.value =  {
      name:"",
      email:"",
      password:"",
      phone:"",
      location:"",
      avatar:""
    };
  }

  const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = reject;
  });

  const inputAvatarFileHandler= () =>{
    avatarInput.value.click();
  }

  const uploadAvatarFileHandler = async (event) =>{
    const avatarFile = event.target.files[0];
    user.value.avatar = await toBase64(avatarFile) as string;
  }

  const validateEmail = (email)=> {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
  }

  const validatePhoneNumber = (phone)=>{
    const regexPhoneNumber = /(84|0[3|5|7|8|9])+([0-9]{8})\b/g;
    return phone.match(regexPhoneNumber) ? true : false;
  }

  const validateLoginInfo = () =>{
    if(validateEmail(user.value.email) && user.value.email.length < 50 && user.value.password.length < 50){
      tabNumber.value++
    }
  }

  const createAccountClickHandler = async () =>{
    const account = {
      Email: user.value.email,
      password: user.value.password,
      Name: user.value.name,
      PhoneNumber: user.value.phone,
      Address: user.value.location,
      Avatar:user.value.avatar
    }

    const result = await accountRepository.signUp(account)
    if(result.code === STATUS_CODE.CONFLICT ){
      dialog.show({
        type:DIALOG_TYPE.MESSAGE,
        header:'Lỗi',
        message:"Sỗ điện thoại hoặc email đã được đăng kí rồi.",
      },()=>{tabNumber.value = 0; user.value={name:"",
          email:"",
          password:"",
          phone:"",
          location:"",
          avatar:""
        }})
    }
    
    else{
      dialog.show({
        type:DIALOG_TYPE.MESSAGE,
        header:"Thành Công",
        message:"Hãy kiểu tra email của bạn, chúng tôi đã gửi mail xác thực."
      },()=>{isLogin.value = true})
    }
  }
  const loginClickHandler = async() =>{
    const result = await accountRepository.login({
      Email:user.value.email,
      password:user.value.password
    });
    if(result.code === STATUS_CODE.BAD_REQUEST){
      dialog.show({
        type:"message",
        header:"Lỗi",
        message: "Email hoặc mật khẩu không đúng."
      })
    }
    else if(result.code === STATUS_CODE.UNAUTHOR){
      dialog.show({
        type:"message",
        header:"Lỗi",
        message: "Tài Khoản của bạn chưa được xác thực."
      })
    }
    else if(result.code === STATUS_CODE.OK){
      dialog.show({
        type:"message",
        header:"Thành công",
        message: "Đăng nhập thành cônng",
      });
      const payload = result.payload;
      cookies.set("token",payload);
      router.push('/');
      mainMenuStore.getMe();
    }
    isLogin.value = true;
      user.value =  {
      name:"",
      email:"",
      password:"",
      phone:"",
      location:"",
      avatar:""
    };
  }

  onMounted(()=>{
    if(route.name == "Login"){
      isLogin.value = true;
    }else{
      isLogin.value = false;
    }
  })

</script>

<style scoped lang="scss">
.half-screen-image{
  width:auto; 
  height:100vh;
  object-fit: cover;
}

.tab-transition{
  @apply transition-all
}

.v-enter-active,
.v-leave-active {
  transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
</style>