<script setup>
import { ref } from 'vue'
import { sidebarWidth } from '@/components/layout/sidebars/state'
import WFSidebar from '@/components/layout/sidebars/WFSidebar.vue'
import WFHeader from '@/components/layout/headers/WFHeader.vue'

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
  <WFHeader/>
  <!-- <WFSidebar /> -->
  <div class="router" :style="{ 'margin-left': sidebarWidth }">
    <router-view></router-view>
  </div>
</template>

<style scoped lang="scss">
.router {
  background-color: var(--primary);
  width: calc(100% - sidebarWidth);
  height: 100%;
}
</style>
