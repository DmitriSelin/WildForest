<script setup>
import { ref } from 'vue'
import { sidebarWidth } from '@/components/layout/sidebars/state'
import WFSidebar from '@/components/layout/sidebars/WFSidebar.vue'
import WFHeader from '@/components/layout/headers/WFHeader.vue'

const isOpenedSidebar = ref(true)

const clickOnBar = () => {
  isOpenedSidebar.value = !isOpenedSidebar.value
}
</script>

<template>
  <WFHeader @barClick="clickOnBar" />
  <WFSidebar :isOpen="isOpenedSidebar" />
  <div class="router" :style="{ 'margin-left': isOpenedSidebar ? sidebarWidth : 0 }">
    <router-view></router-view>
  </div>
</template>

<style scoped lang="scss">
.router {
  background-color: var(--primary);
  width: calc(100% - sidebarWidth);
  height: calc(100% - 77px);
  margin-top: 77px;
}
</style>
