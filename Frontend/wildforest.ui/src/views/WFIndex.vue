<script setup>
import { ref } from 'vue'
import { sidebarWidth } from '@/components/layout/sidebars/state'
import WFSidebar from '@/components/layout/sidebars/WFSidebar.vue'
import WFHeader from '@/components/layout/headers/WFHeader.vue'

const isClosedSidebar = ref(true)

const clickOnBar = () => {
  isClosedSidebar.value = !isClosedSidebar.value
}
</script>

<template>
  <WFHeader @barClick="clickOnBar" />
  <WFSidebar :isClosed="isClosedSidebar" />
  <div class="router" :style="{ 'margin-left': sidebarWidth }">
    <router-view></router-view>
  </div>
</template>

<style scoped lang="scss">
.router {
  background-color: var(--primary);
  width: calc(100% - sidebarWidth);
  height: 100%;
  margin-top: 77px;
}
</style>
