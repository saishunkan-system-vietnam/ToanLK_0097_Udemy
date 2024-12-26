<template>
  <div
    class="flex justify-center pt-4"
  >
    <div
      class="white tex-white border round-page-button"
      @click="changePage(modelValue-2)"
    >
      <CIcon
        icon="Chevron Left" 
      />
    </div>
    <template
      v-for="(value,index) in props.quantityOfPages"
      :key="index"
    >
      <div
        v-if="modelValue == 0 && index == 1 || modelValue == 0 && index == 2 ||
          modelValue == quantityOfPages-1 && index == quantityOfPages - 2 ||
          modelValue == quantityOfPages-1 && index == quantityOfPages - 3 || modelValue - 1 <= index && index <= modelValue + 1"
        class="round-page-button border"
        :class="{'active': (index == modelValue)}"
        @click="changePage(index)"
      >
        {{ index + 1 }}
      </div>
    </template>
    
    <div class="white tex-white border round-page-button">
      <CIcon
        icon="Chevron Right"
        @click="changePage(modelValue+2)"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import {onMounted, ref} from 'vue';
  const props = defineProps({
    quantityOfPages: Number,
    modelValue:Number
  });
  const emits = defineEmits(['update:modelValue'])
  const changePage = (index) =>{
    if(index > props.quantityOfPages-1 || index < 0){
      return;
    }
    emits('update:modelValue',index);
  }
</script>

<style scoped>
.round-page-button {
  @apply rounded-full w-8 h-8 flex items-center justify-center m-1 cursor-pointer select-none
}

.round-page-button.active{
  @apply bg-[#8071b3] text-white

}
</style>