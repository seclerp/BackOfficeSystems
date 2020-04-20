import Vue from 'vue'
import VueRouter from 'vue-router'
import SumOfInventory from '../views/SumOfInventory.vue'
import CreateBrand from "../views/CreateBrand";

Vue.use(VueRouter)

const linkActiveClass = 'active-link'

const routes = [
  {
    path: '/',
    name: 'Sum of inventory',
    component: SumOfInventory
  },
  {
    path: '/brand/new',
    name: 'Create brand',
    component: CreateBrand
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
  linkActiveClass
})

export default router
