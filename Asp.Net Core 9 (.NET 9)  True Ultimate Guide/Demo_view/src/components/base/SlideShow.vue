<template>
  <q-carousel
    :model-value="props.modelValue"
    animated
    arrows
    navigation
    infinite
    transition-next="jump-left"
    transition-prev="jump-right"
    :class="props.class"
    v-bind="$attrs"
    class=" overflow-hidden"
    @update:model-value="emitsValue"
  >
    <template v-if="props.mode == SLIDE_SHOW_MODE.ONLY_IMAGE">
      <q-carousel-slide
        v-for="(slide, index) in props.items"
        :name="index"
        class="!p-0 overflow-hidden"
      >
        <img
          :src="slide.src"
          :alt="slide.src"
          class="img-zoom"
        >
      </q-carousel-slide>
    </template>

    <template v-else-if="props.mode == SLIDE_SHOW_MODE.CONTAIN_SLOT">
      <slot />
    </template>
  </q-carousel>
</template>
<script setup lang='ts'>
import { SLIDE_SHOW_MODE } from 'common/enum';
const props = defineProps({
  modelValue: {
    required: true,
    type: Number,
    default: 1
  },
  items: {
    required: true,
    type: Object,
    default: () => { }
  },
  class: {
    required: false,
    type: String,
    default: ''
  },
  mode: {
    required: false,
    type: String,
    default: 'image'
  }
})


const emitsValue = (value) => {
  emits('update:modelValue', value);
}

const emits = defineEmits(["update:modelValue"]);


</script>
<style scoped lang='scss'>
.img-zoom {
  height: auto;
  width: 100%;
  
  transition: transform .6s;
  overflow: hidden;
  @apply object-center
}

.img-zoom:hover {
  transform: scale(1.1);
}
</style>
