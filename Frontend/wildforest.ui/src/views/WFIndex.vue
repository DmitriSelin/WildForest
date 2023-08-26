<script setup>
import { ref } from 'vue'
import WFSidebar from '@/components/layout/WFSidebar.vue'

const links = [
  { name: 'sidebarLink', label: 'Main', href: '/', checked: false, icon: 'house-chimney' },
  { name: 'sidebarLink', label: 'Weather', href: '/', checked: false, icon: 'cloud-showers-heavy' },
  { name: 'sidebarLink', label: 'Comments', href: '/comments', checked: false, icon: 'comment' }
]

const selectedBar = ref(links[0].label)
const changeBar = (label) => {
  selectedBar.value = label
}
</script>

<template>
  <WFSidebar :links="links" :selectedBar="selectedBar" @changeBar="changeBar" />
  <div class="router">
    <router-view></router-view>
  </div>
</template>

<style scoped lang="scss">
.router {
  width: 100%;
  height: 100%;
  background-color: gray;
}
</style>
