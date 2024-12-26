import { createRouter, createWebHistory } from 'vue-router'
import {routes} from 'src/router/routes';
const router = createRouter({
  scrollBehavior(to, from, savedPosition) {
    // always scroll to top
    return { el: to.hash,left:0, top: 0}
    // return {x:0,y:0}
  },

  history: createWebHistory(import.meta.env.BASE_URL),
  //TODO: get rid of 'as any'
  routes:routes as any,
 
})

router.afterEach((to, from) => {
  window.scrollTo(0, 0);
})

export default router
