<script setup>
import { collapsed, toggleSidebar, sidebarWidth } from './state';
import WFSidebarLink from './WFSidebarLink.vue';

const props = defineProps({
  isClosed: {
    type: Boolean,
    default: false
  }
})
</script>

<template>
  <Transition name="fade">
    <nav class="sidebar" :style="{ width: sidebarWidth }" v-if="isClosed">

      <WFSidebarLink to="/weather" icon="fa-solid fa-house-chimney">Home</WFSidebarLink>
      <WFSidebarLink to="/weather/forecast" icon="fas fa-cloud-showers-heavy">Weather</WFSidebarLink>
      <WFSidebarLink to="/weather/comments" icon="fas fa-comment">Comments</WFSidebarLink>

      <span @click="toggleSidebar" class="collapse-icon" :class="{ 'rotate-180': collapsed }">
        <font-awesome-icon icon="fa-solid fa-angles-left" />
      </span>
    </nav>
  </Transition>
</template>

<style lang="scss" scoped>
.fade-enter-active {
  transition: opacity 0.2s ease-in;
}

.fade-leave-active {
  transition: opacity 0.1s ease-out;
}

.fade-enter {
  opacity: 0;
}

.fade-leave-to {
  opacity: 0;
}

.sidebar {
  color: var(--white);
  background-color: var(--second);
  position: fixed;
  float: left;
  z-index: 1;
  top: 77px;
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
    padding: 15px 22px;
    color: var(--white);
    transition: 0.3s linear;
    cursor: pointer;
  }
}

.rotate-180 {
  transform: rotate(180deg);
  transition: 0.2s linear;
}
</style>
