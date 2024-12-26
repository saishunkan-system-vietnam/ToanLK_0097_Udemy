<template>
  <span :class="[`material-icons ${props.class}`]">{{ iconWithSnakeCase }}</span>
</template>
<script setup lang='ts'>
import { onMounted, ref } from 'vue';
const props = defineProps({
  // name of icon 
  // related to name of icon 
  // see: https://fonts.google.com/icons
  icon: {
    require: true,
    type: String,
    default: ''
  },
  class: {
    require: false,
    type: String,
    default: ''
  },

});
const iconWithSnakeCase = ref("");
onMounted(() => {
  iconWithSnakeCase.value = props.icon.toLowerCase().replace(/\s+/g, '_');
})
</script>