<script setup>
import WFSidebar from '@/components/layout/WFSidebar.vue'

const links = [
  { name: 'Main', href: '/', checked: true, icon: 'house-chimney' },
  { name: 'Weather', href: '/weather', checked: false, icon: 'cloud-showers-heavy' },
  { name: 'Comments', href: '/comments', checked: false, icon: 'comment' }
]
</script>

<template>
  <WFSidebar :links="links" />
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
