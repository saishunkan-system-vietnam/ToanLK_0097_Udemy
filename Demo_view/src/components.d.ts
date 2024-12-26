import CIcon from "base/CIcon.vue";
declare module '@vue/runtime-core' {
  export interface GlobalComponents {
    CIcon: typeof CIcon
  }
}