import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const  useMainMenuStore = defineStore('mainMenu', () => {
  const myCallback = null;
  const doFunction = ()=>{
    myCallback();
  }

  const getMe = null; 
  
  return {  doFunction, myCallback, getMe}
})
