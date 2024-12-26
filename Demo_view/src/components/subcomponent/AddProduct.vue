<template>
  <div>
    <div class="font-extrabold text-xl pb-4 flex">
      <div>
        <div class="mb-3">
          <span v-if="!props.isUpdate">Add Product</span>
          <span v-else>Update Product</span>
        </div>
        <div class="">
          <q-btn
            class="!border-none"
            @click="backToList"
          >
            <CIcon icon="Arrow Back" />
            Go back
          </q-btn>
        </div>
      </div>

      <div class="ml-[auto]">
        <q-btn
          class="!bg-green-600 text-white mr-4"
          @click="() => props.isUpdate ? updateImageHandler() : saveProductHandler()"
        >
          &nbsp;Save&nbsp;
        </q-btn>
        <q-btn
          class="!bg-slate-20"
          @click="clearProductHandler"
        >
          Clear
        </q-btn>
      </div>
    </div>
    <div class="row-input">
      <q-input
        v-model="product.name"
        outlined
        label="Name"
        class="w-full"
        dense
      />
    </div>
    <div class="row-input">
      <q-input
        v-model="product.code"
        outlined
        label="Code"
        class="w-full pr-2"
        dense
      />
      <q-input
        v-model="product.price"
        outlined
        type="number"
        label="Price"
        class="w-full"
        dense
      />
    </div>
    <div class="row-input">
      <q-input
        v-model="product.guarantee"
        outlined
        label="Guarantee "
        class="w-full pr-2"
        dense
      />
      <q-input
        v-model="product.keyword"
        outlined
        label="Key word"
        class="w-full"
        dense
      />
    </div>
    <div class="row-input">
      <!-- <q-color
        v-model="hex"
        class="my-picker"
      /> -->
      <div class="w-full">
        <div />
        <q-select
          v-model="product.type"
          class="w-full pr-2"
          outlined
          dense
          option-label="name"
          :options="props.typeList"
          label="Product Type"
        />
      </div>
      <div class="w-full">
        <q-input
          v-model="product.mounts"
          type="number"
          class="w-full"
          outlined
          dense
          label="mount"
        />
      </div>
    </div>
    <q-btn
      class="mt-2 !bg-[#8071b3] text-white"
      @click="inputFileClickHandler"
    >
      Add Image
    </q-btn>
    <input
      ref="productImageInput"
      type="file"
      multiple
      class="invisible"
      @change="productImageHandler"
    >
    <div class="border w-full min-h-[200px] border-[#c2c2c2] my-2 rounded-md grid grid-cols-4 gap-4 p-2">
      <div
        v-for="(file, index) in allImage"
        :key="index"
        class="relative h-fit image-overlay"
      >
        <img
          :src="props.updateProduct!=null && file.includes(props.updateProduct[
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
    <div class="">
      <h6 class="">
        Description
      </h6>
      <EditorPages
        ref="descriptionEditor"
        v-model="product.description"
        placeholder="enter product description..."
      />
    </div>
    <div class="h-[400px]" />
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import type {Ref} from 'vue';
import { toBase64 } from 'src/utils/common';
import EditorPages from 'tools/EditorPages.vue'
import productRepository from 'repository/productRepository';
import type {IProduct} from 'interface/ProductInterface'; 
import _ from 'lodash';

const props = defineProps<{
  isUpdate: Boolean,

  updateProduct: IProduct
  typeList:Array<any>
}>();

const product:Ref<IProduct> = ref({
  name: '',
  vietnameseName: "",
  code: "",
  price: 0,
  keyword: "",
  guarantee: 0,
  description: "",
  images: "",
  type: "",
  mounts:0

});

const updateImageHandler = () => {
  productRepository.update({
    product: product.value,
    Images: allImage.value
  })
}

const emits = defineEmits(["changeToList"]);

const descriptionEditor = ref();
const updateImageList = ref([]);
const productImageList = ref([]);
const productImageInput = ref();

const backToList = () => {
  emits("changeToList");
}
const inputFileClickHandler = () => {
  productImageInput.value.click();
}

const productImageHandler = async (event) => {
  const files = event.target.files;
  for (const file of files) {
    productImageList.value.push(await toBase64(file));
  }
  event.target.value = null;
}

const deleteImage = (file, index) => {
  if (file.includes(props.updateProduct["id"])) {
    updateImageList.value.splice(index, 1);
  } else {
    productImageList.value.splice(index - updateImageList.value.length, 1);
  }
}



const saveProductHandler = () => {
  // call api to save
  const productReq = _.clone(product.value);

  productReq.images = productImageList.value;
  productReq.typeId = productReq.type.id;
  delete productReq.type;
  productRepository.add(productReq);
  clearProductHandler();
}
const clearProductHandler = () => {
  product.value = {
    name: '',
    vietnameseName: "",
    code: "",
    price: 0,
    keyword: "",
    guarantee: 0,
    description: "",
    images: "",
    type: "",
    mounts:0
  };
  productImageList.value = [];
  descriptionEditor.value?.clearText();
}

const allImage = computed(() => {
  const value = updateImageList.value?.concat(productImageList.value);
  return value
})

onMounted(() => {
  if (props.isUpdate) {
    product.value = props.updateProduct;
    descriptionEditor.value.setText(product.value.description);
    updateImageList.value = product.value["images"].split(",");
    product.value.type = props.typeList.find((x)=>x.id == product.value.typeId)
    return;
    
  }
  product.value.type = props.typeList[0];

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
  /* @apply !visible; */
  opacity: 1;
  background-color: rgba(22, 14, 14, 0.548);

}

.overlay-div {}
</style>