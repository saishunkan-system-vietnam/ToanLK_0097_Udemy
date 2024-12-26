<template>
  <div class="wrapper">
    <div />
    <div>
      <div ref="editorElement" />
    </div>
  </div>
</template>

<script setup lang="ts">

import { onMounted, ref, watch } from 'vue';
import Quill  from 'quill';
import "quill/dist/quill.core.css";
import "quill/dist/quill.bubble.css";
import "quill/dist/quill.snow.css";
import ResizeModule from "@botom/quill-resize-module";

const props = defineProps({
  modelValue:String,
  placeholder:{
    type:String,
    default: 'type in here'
  }
});

const editor = ref();

const emits = defineEmits(['update:modelValue']);

const saveValue = ()=>{
  emits('update:modelValue',editor.value.root.innerHTML);
}

const editorElement = ref();
onMounted(()=>{
  const toolbarOptions = [
  ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
  ['blockquote', 'code-block'],

  [{ 'header': 1 }, { 'header': 2 }],               // custom button values
  [{ 'list': 'ordered'}, { 'list': 'bullet' }],
  [{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript
  [{ 'indent': '-1'}, { 'indent': '+1' }],          // outdent/indent
  [{ 'direction': 'rtl' }],                         // text direction

  [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
  [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

  [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
  [{ 'font': [] }],
  [{ 'align': [] }],
  ['clean'],                             // remove formatting button
['image'],
['video']

];
  Quill.register("modules/resize", ResizeModule);
  editor.value =new Quill(editorElement.value,{
    theme:'snow',
    //debug: 'info',
    modules: {
      toolbar: toolbarOptions,
       resize: {
        showSize: true,
        toolbar: {
        },
        locale: {
        },
    } },
    placeholder: props.placeholder,
  });
  editor.value.root.innerHTML = props.modelValue;
  editor.value.on("text-change", function () {
    emits('update:modelValue',editor.value.root.innerHTML);
  });
})

const clearText = () =>{
  editor.value.root.innerHTML = "";
}

const setText = (richText) => {
  editor.value.root.innerHTML = richText;
}

defineExpose({
  clearText,
  setText
})
// watch(
//   () => props.modelValue,
//   (newContent) => {
//     if (editor.value) editor.value.root.innerHTML = newContent;
//   },
//   { deep: true },
// );


</script>

<style scoped>
.preview-box{
  width:100%;
  min-height:120px;
  border-radius: 4px;
  border:1px solid var(--border-gray);
}

.editor-box{
  border-radius: 4px;
  border:1px solid var(--border-gray); 
}

.add-button{
  margin:8px;
}
</style>