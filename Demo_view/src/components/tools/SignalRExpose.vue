<template>
  <div>
    hello world from signalR expose
    <q-btn @click="SendMessage">
      Click Me
    </q-btn>
  </div>
</template>

<script setup lang="ts">
import { useSignalR } from '@dreamonkey/vue-signalr';
import { inject, onMounted } from 'vue';
import CommentRepository from 'repository/commentRepository';


onMounted(()=>{
  const signalr = useSignalR();
  const message = "this is an message";
  signalr.invoke('GotComment', message );

  signalr.on('MessageReceived', ( user,message) => {
    /* do stuff */
 
  });
})
const SendMessage = ()=>{
  CommentRepository.SendMessage();
}

</script>

<style scoped>

</style>