import LandingPage from 'pages/LandingPage.vue';
import VerifyAccount from 'components/tools/VerifyAccount.vue';
import LoginPage from 'pages/LoginPage.vue';
import AdminPage from 'pages/AdminPage.vue';
import ProductDetail from 'pages/ProductDetail.vue';
import ProductList from 'pages/ProductList.vue'
import ProductLayout from 'src/components/pages/ProductLayout.vue';
import CartPage from 'pages/CartPage.vue';
import UserDetail from 'pages/UserDetail.vue';
import ChangePassword from 'pages/ChangePassword.vue';
import PageNews from 'pages/PageNews.vue';
import NewsDetail from 'pages/NewsDetail.vue';
import PaymentAction from 'pages/PaymentAction.vue';

export const routes = [
  {
    path: "/",
    name: "Trang Chủ",
    component: LandingPage,
    meta: {
      show: true,
      showMainMenu: true,
      showSidebar: false
    },
  },
  {
    path: "/user",
    name:"User",
    component: UserDetail,
    meta: {
      showSortBy: false,
      showMainMenu:true,
      showSidebar:false
    },
  },
  {
    path:"/password",
    name:"changePassword",
    component:ChangePassword,
    meta:{
      show:false,
      showSortBy:false,
      showMainMenu:true,
      showSidebar:false,
    }
  },
  {
    path: "/product",
    name: "Product",
    component: ProductLayout,
    meta: {
      show: false,
      showMainMenu: true,
      showSidebar: true,
    },
    
    children: [
      {
        path: "list",
        component: ProductList,
        meta: {
          showSortBy: true,
        }
      },
      {
        path: "detail/:id",
        component: ProductDetail,
        meta: {
          showSortBy: false,
        }
      },
      {
        path:"keyboard",
        name:"keyboard",
        component: ProductList,
        meta: {
          showSortBy: true,
        }
      },
      {
        path:"mouse",
        name:"mouse",
        component: ProductList,
        meta: {
          showSortBy: true,
        }
      },
      {
        path:"kit-keyboard",
        name:"kit",
        component: ProductList,
        meta: {
          showSortBy: true,
        }
      },
      {
        path:"switch",
        name:"switch",
        component: ProductList,
        meta: {
          showSortBy: true,
        }
      }
    ]
  },
  {
    path: "/keyboard",
    name: "Bàn Phím",
    redirect:{
      path:"/product/keyboard",
    },
    meta: {
      show: true,
      showMainMenu: true,
      showSidebar: false
    },
  },
  {
    path: "/kit-keyboard",
    name: "Kit Bàn Phím",
    redirect:{
      path:"/product/kit-keyboard",
    },
    meta: {
      show: true,
      showMainMenu: true,
      showSidebar: false
    }
  },
  {
    path: "/mouse",
    name: "Chuột",
    redirect:{
      path:"/product/mouse",
    },
    meta: {
      show: true,
      showMainMenu: true,
      showSidebar: false

    }
  },
  {
    path: "/switch",
    name: "Switch",
    redirect:{
      path:"/product/switch",
    },
    meta: {
      show: true,
      showMainMenu: true,
      showSidebar: false
    }
  },
  {
    path:"/news",
    name:"Tin Tức",
    component:PageNews,
    meta:{
      show:true,
      showMainMenu:true,
      showSidebar:false
    }
  },
  {
    path:"/payment",
    name:"Thanh Toán",
    component:PaymentAction,
    meta:{
      show:false,
      showMainMenu:true,
      showSidebar:false
    }
  },
  {
    path: "/support",
    name: "Hỗ Trợ",
    meta: {
      show: true,
      showMainMenu: true,
      showSidebar: false
    }
  },
  {
    path: "/cart",
    name: "Cart",
    component:CartPage,
    meta: {
      show: false,
      showMainMenu: true,
      showSidebar: false
    }
  },
  {
    path: "/detail",
    name: "Detail",
    meta: {
      show: false,
      showMainMenu: true,
      showSidebar: false
    },
    component: ProductDetail
  },
  {
    path: "/admin",
    name: "Admin",
    meta: {
      show: false,
      showMainMenu: false,
      showSidebar: false
      ,
    },
    component: AdminPage
  },
  {
    path: "/verify/:id",
    name: "verify account",
    show: false,
    meta: {
      show: false,
      showMainMenu: false,
      showSidebar: false
    },
    component: VerifyAccount
  },
   {
    path: "/product-detail/:id",
    name: "Product Detail",
    show: false,
    meta: {
      show: false,
      showMainMenu: false,
      showSidebar: false
    },
    component: ProductDetail
  },
  {
    path: "/news-detail/:id",
    name: "News Detail",
    show: false,
    meta: {
      show: false,
      showMainMenu: true,
      showSidebar: false
    },
    component: NewsDetail
  },
  {
    name: "Sign Up",
    path: "/sign-up",
    show: false,
    meta: {
      show: false,
      showMainMenu: false,
      showSidebar: false

    },
    component: LoginPage,
  },
  {
    name: "Login",
    path: "/login",
    show: false,
    meta: {
      show: false,
      showMainMenu: false,
      showSidebar: false

    },
    component: LoginPage,
  },
  {
    path: '/:catchAll(.*)',
    redirect: '/',
  }
]