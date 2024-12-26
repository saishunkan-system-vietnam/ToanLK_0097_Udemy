<template>
  <div>
    <div class="font-extrabold text-xl pb-4 flex">
      <div>
        <div class="mb-3">
          <span v-if="!props.isUpdate">Thêm Phân Loại</span>
          <span v-else>Chỉnh Sửa Phân Loại</span>
        </div>
        <div class="">
          <q-btn
            class="!border-none"
            @click="backToList"
          >
            <CIcon icon="Arrow Back" />
            Về Danh Sách
          </q-btn>
        </div>
      </div>

      <div class="ml-[auto]">
        <q-btn
          class="!bg-green-600 text-white mr-4"
          @click="() => props.isUpdate ? updateTypeHandler() : saveType()"
        >
          &nbsp;Lưu&nbsp;
        </q-btn>
        <q-btn
          class="!bg-slate-20"
          @click="clearTypeHandler"
        >
          Làm Mới
        </q-btn>
      </div>
    </div>
    <div class="row-input">
      <q-input
        v-model="type.name"
        outlined
        label="Tên Phân Loại"
        class="w-full"
        dense
      />
    </div>
    <div class="row-input">
      <q-btn
        class="mt-2 !bg-[#8071b3] text-white"
        :disable="typeImageList.length > 0"
        @click="inputFileClickHandler"
      >
        Thêm Ảnh
      </q-btn>
      <input
        ref="typeImage"
        type="file"
        multiple
        class="invisible"
        @change="typeImageHandler"
      >
    </div>
    <div class="border w-full min-h-[200px] border-[#c2c2c2] my-2 rounded-md grid grid-cols-4 gap-4 p-2">
      <div
        v-for="(file, index) in allImage"
        :key="index"
        class="relative h-fit image-overlay"
      >
        <img
          :src="props.updateType!=null && file.includes(props.updateType[
            'id']) ? `https://localhost:7082/${file}` : file"
          class="h-[auto]"
          :alt="index.toString()"
          accept="image/*"
        >
        <div class="absolute top-0 left-0 right-0 bottom-0 bg-slate-50 overlay-div flex items-center justify-center">
          <CIcon
            icon="Delete"
            class="text-white !text-[34px] p-4"
            @click="deleteImage(file, index)"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import typeRepository from 'repository/typeRepository';
import { toBase64 } from 'utils/common';
const props = defineProps(["isUpdate","updateType"])
import { useDialogStore } from 'src/stores/dialog';
import { DIALOG_TYPE } from 'common/enum';

const dialog = useDialogStore();

const type = ref({
  name: '',
});


const allImage = computed(() => {
  const value = updateImageList.value?.concat(typeImageList.value);
  return value
})

const emits = defineEmits(["changeToList"]);
const updateImageList = ref([]);

const updateTypeHandler = async () =>{
  const result = await typeRepository.update(
    {
      type:type.value,
      images:allImage.value
    });
  if(result.payload){
    dialog.show({
      type:DIALOG_TYPE.MESSAGE,
      header:'Thành Công',
      message:"Lưu thay đổi thành công",
    },()=>{
      clearTypeHandler();
    emits("changeToList");
    });
  }
}

const backToList = () => {
  emits("changeToList");
}

const saveType = async () => {
  // call api to save
  const typeValue = {};
  typeValue['type'] = type.value;
  typeValue["images"] = typeImageList.value;
  const result = await typeRepository.add(typeValue);
  if(result.payload){
    await dialog.show({
    type:DIALOG_TYPE.MESSAGE,
    header:'Thành Công',
    message:"Thêm mới thành công",
  },()=>{
    clearTypeHandler();
    emits("changeToList");
  });

  }
}

const clearTypeHandler = () => {
  type.value = {
    name: '',
  };
}

const typeImage = ref(); 
const typeImageList = ref([]);

const inputFileClickHandler = () => {
  typeImage.value.click();
}

const typeImageHandler = async (event) => {
  const files = event.target.files;
  for (const file of files) {
    typeImageList.value.push(await toBase64(file));
  }
  event.target.value = null;
}

const deleteImage = (file,index) =>{
  if (file.includes(props.updateType["id"])) {
    updateImageList.value.splice(index, 1);
  } else {
    typeImageList.value.splice(index - updateImageList.value.length, 1);
  }
}

onMounted(() => {
  if (props.isUpdate) {
    type.value = props.updateType;
    updateImageList.value = type.value["images"].split(",");
  }

})

</script>

<style scoped>
.row-input {
  @apply py-1 flex
}

.overlay-div {
  @apply transition-all;
  opacity: 0;
}

.image-overlay:hover .overlay-div {
    opacity: 1;
    background-color: rgba(22, 14, 14, 0.548);
}

</style>