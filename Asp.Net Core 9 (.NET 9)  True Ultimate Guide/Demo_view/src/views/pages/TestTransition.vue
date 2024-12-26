<template>
  <TransitionGroup>
    <div
      v-if="isShow"
      class="h-[300px]"
    >
      show ere
    </div>
  </TransitionGroup>
  <button @click="epand">
    click
  </button>
</template>
<script setup lang="ts">
import {ref} from 'vue';
const isShow = ref(false);
const epand= () =>{
  isShow.value = !isShow.value;
}


</script>
<style>
.animation{
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