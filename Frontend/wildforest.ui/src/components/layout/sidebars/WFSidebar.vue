<script setup>
import { collapsed, toggleSidebar, sidebarWidth } from './state';
import WFSidebarLink from './WFSidebarLink.vue';
</script>

<template>
  <nav class="sidebar" :style="{ width: sidebarWidth }">

    <WFSidebarLink to="/" icon="fa-solid fa-house-chimney">Home</WFSidebarLink>
    <WFSidebarLink to="/a" icon="fas fa-cloud-showers-heavy">Weather</WFSidebarLink>
    <WFSidebarLink to="/aa" icon="fas fa-comment">Comments</WFSidebarLink>

    <span @click="toggleSidebar" class="collapse-icon" :class="{ 'rotate-180': collapsed }">
      <font-awesome-icon icon="fa-solid fa-angles-left" />
    </span>
  </nav>
</template>

<style lang="scss" scoped>
.sidebar {
  color: var(--white);
  background-color: var(--second);
  position: fixed;
  float: left;
  z-index: 1;
  top: 0;
  left: 0;
  bottom: 0;
  padding: 60px 5px 5px 5px;
  transition: 0.3s ease;
  display: flex;
  flex-direction: column;

  .collapse-icon {
    position: absolute;
    left: 0;
    top: 0;
    padding: 15px;
    color: var(--white);
    transition: 0.2s linear;
    cursor: pointer;
  }
}

.rotate-180 {
  transform: rotate(180deg);
  transition: 0.2s linear;
}
</style>
