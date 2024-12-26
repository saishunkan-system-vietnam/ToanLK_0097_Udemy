<template>
  <div class="max-w-[1280px] mx-[auto] px-2">
    <div class="pt-4">
      <h6>Thông tin người dùng</h6>
      <div class="pb-2">
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
              <img
                :src="user.avatar == ''|| user.avatar == null? 'images/login/default-avatar.jpg' : user.avatar"
                class="object-cover"
              >
            </q-avatar>
          </q-btn>
        </div>
      </div>
      <div class="">
        <q-input
          v-model="user.name"
          outlined
          label="Name"
          :rules="[ 
            val=>val.length != 0 || 'Xin hãy nhập tên của bạn' 
          ]"
          dense
        />
      </div>
      <div class=""> 
        <q-input
          v-model="user.email"
          outlined
          disable
          label="Email"
          :rules="[ 
            val=>val.length != 0 || 'Xin hãy nhập địa chỉ thư điện tử' 
          ]"
          dense
        />
      </div>
      <div class="">
        <q-input
          v-model="fakePassword"
          outlined
          disable
          type="password"
          label="Password"
          :rules="[ 
            val=>val.length != 0 || '' 
          ]"
          dense
        />
      </div>
      <div class=""> 
        <q-input
          v-model="user.phoneNumber"
          outlined
          label="Phone"
          :rules="[ 
            val=>val.length != 0 || 'Xin hãy nhập số điện thoại' 
          ]"
          dense
        />
      </div>
      <div class="">
        <q-select
          v-model="provinceId"
          outlined
          dense
          :options="provinceList"
          option-label="province_name"
          label="Tỉnh Thành"
          :rules="[ 
            val=>val || 'Xin hãy nhập thông tin tỉnh thành' 
          ]"
        />
      </div>
      <div class="">
        <q-select
          v-model="districtId"
          outlined
          dense
          :options="districtList"
          option-label="district_name"
          label="Quận Huyện"
          :rules="[ 
            val=>val || 'Xin hãy nhập thông tin quận huyện' 
          ]"
        />
      </div>
      <div class="">
        <q-select
          v-model="wardId"
          outlined
          dense
          :options="wardList"
          option-label="ward_name"
          label="Phường Xã"
          :rules="[ 
            val=>val || 'Xin hãy nhập thông tin phường xã' 
          ]"
        />
      </div>
    </div>
    <div class="">
      <q-input
        v-model="user.address"
        outlined
        label="Địa chỉ chi tiết"
        dense
        :rules="[ 
          val=>val.length != 0 || 'Xin hãy nhập thông tin địa chỉ' 
        ]"
      />
    </div>
    <div class="ml-auto w-fit">
      <q-btn
        color="primary"
        label="Lưu thay đổi"
        class=""
        @click="changeUserDetail()"
      />
    </div>
  </div>
  <div class="fixed bottom-0 left-0 right-0">
    <footerInfo />
  </div>
</template>

<script lang="ts" setup>
import FooterInfo from 'components/subpage/FooterInfo.vue';
import {onMounted, ref, computed ,watch} from 'vue';
import accountRepository from 'repository/accountRepository';
import provinceApi from 'repository/provinceApi';
import { disconnect } from 'process';
import { useDialogStore } from 'src/stores/dialog';

import _ from 'lodash';
import { DIALOG_TYPE } from 'common/enum';

const provinceId = ref(); 
const districtId = ref();
const wardId = ref();
const dialog = useDialogStore();


const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = reject;
  });

const user = ref({
  name:"",
  email:"",
  password:"",
  phoneNumber:"",
  address:"",
  avatar:""
});

const fakePassword = ref("dummypassword");
const provinceList = ref([]);
const districtList = ref([]);
const wardList = ref([]);


const uploadAvatarFileHandler = async (event) =>{
    const avatarFile = event.target.files[0];
    user.value.avatar = await toBase64(avatarFile) as string;
  }

const inputAvatarFileHandler= () =>{
  avatarInput.value.click();
}

const avatarInput = ref();

const changeUserDetail = async() =>{
  user.value.provinceId = provinceId.value.province_id;
  user.value.districtId = districtId.value.district_id;
  user.value.wardId = wardId.value.ward_id;
  const result = await accountRepository.update(user.value);
  const value = await dialog.show({
    type:DIALOG_TYPE.MESSAGE,
    header:'Thành Công',
    message:"Thay đổi thông tin người dùng thành công",
  });
}

onMounted(async()=>{
  const currentUser = await accountRepository.getMe();
  if(currentUser.payload != null){
    user.value = currentUser.payload;
  }

  // get province list
  const result = await provinceApi.GetProvince()
  provinceList.value = result.data.results;
  if(user.value.provinceId){
    provinceId.value = _.cloneDeep(provinceList.value.find(x=>x.province_id == user.value.provinceId));
  }
});

const a = ref();
watch(provinceId,async(newValue,oldValue)=>{
  if(!newValue){
    districtList.value = [];
  }
  const result = await provinceApi.GetDistrict(newValue.province_id);
  // console.log(_.clone(result.data.results[0]));
  districtList.value = _.cloneDeep(result.data.results);
  a.value = _.cloneDeep(result.data.results);

  if(user.value.districtId && !districtId.value){
    districtId.value = _.clone(districtList.value.find(x=>x.district_id == user.value.districtId));
    return;
  }
  districtId.value = _.cloneDeep(districtList.value[0]);
})

watch(districtId,async(newValue,oldValue)=>{
  if(!newValue){
    wardList.value = [];
  }
  const result = await provinceApi.GetWard(newValue.district_id);
  wardList.value = result.data.results;
  if(user.value.wardId && !wardId.value ){
    wardId.value = _.cloneDeep(wardList.value.find(x=>x.ward_id = user.value.wardId));
    return;
  }
  wardId.value = _.cloneDeep(wardList.value[0]);
});

</script>

<style scoped lang="scss">
</style>