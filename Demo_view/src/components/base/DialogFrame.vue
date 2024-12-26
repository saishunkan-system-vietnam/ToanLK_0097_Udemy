<template>
  <div>
    <q-dialog
      v-for="item in dialogs"
      :model-value="true"
    >
      <template v-if="item.type==DIALOG_TYPE.LOADING">
        <q-spinner
          color="primary"
          size="3em"
        />
      </template>
      <template v-else-if="item.type != DIALOG_TYPE.COMPONENT">
        <div class="bg-white rounded-sm p-6 w-[300px] text-lg">
          <div class="text-bold pb-4">
            {{ item.header }}
          </div>
          <div>
            <div class="pb-4 text-base">
              {{ item.message }}
            </div>
            <div class="text-right">
              <template v-if="item.type== DIALOG_TYPE.CONFIRM">
                <q-btn
                  class="mr-2"
                  @click="okClickHandler"
                >
                  OK
                </q-btn>
                <q-btn @click="dialog.hide()">
                  Cancel
                </q-btn>
              </template>
              <template v-if="item.type== DIALOG_TYPE.MESSAGE">
                <q-btn @click="()=>{dialog.callbackFunc();dialog.hide();}">
                  OK
                </q-btn>
              </template>
            </div>
          </div>
        </div>
      </template>
    </q-dialog>
  </div>
</template>

<script setup lang="ts">
import {useDialogStore} from 'stores/dialog';
import {DIALOG_TYPE} from 'common/enum';
import { ref } from 'vue';

const dialog = useDialogStore();

const dialogs = ref(dialog.dialog)

const okClickHandler = () =>{
  dialog.callbackFunc();
  dialog.hide();
}
</script>

<style scoped>

</style>