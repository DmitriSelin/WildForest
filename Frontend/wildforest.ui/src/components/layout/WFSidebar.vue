<script setup>
import WFSidebarRadiobutton from '@/components/layout/WFSidebarRadiobutton.vue'
import { ref } from 'vue'

const props = defineProps({
  links: {
    type: Array,
    required: true
  }
})

const isClosed = ref(true)

const changeSidebarSize = () => {
  isClosed.value = !isClosed.value;
}
</script>

<template>
  <nav class="sidebar" :class="{'close': isClosed}">
    <div class="header">
      <div class="image-text">
        <span class="image">
          <img src="../../assets/images/logo.ico" alt="logo" />
        </span>
        <div class="header-text" v-if="!isClosed">
          <span class="name">Wild forest</span>
        </div>
      </div>
      <div class="toggle" @click="changeSidebarSize">
        <font-awesome-icon icon="fa-solid fa-bars" class="icon" />
      </div>
    </div>
    <div class="menu-bar">
      <div class="menu">
        <ul class="menu-links">
          <li v-for="link in links" :key="link.name">
            <router-link :to="link.href" class="link">
              <WFSidebarRadiobutton
                :name="link.name"
                :id="link.name"
                :value="link.name"
                :label="link.name"
                :checked="link.checked"
                :icon="link.icon"
                :isClosed="isClosed"
              />
            </router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<style scoped lang="scss">
.sidebar {
  position: fixed;
  top: 0;
  left: 0;
  height: 100vh;
  width: 250px;
  background-color: var(--second);
  padding: 10px 14px;
  transition: all 0.5s ease;
  .header {
    margin-left: 4px;
  }
  .image-text img {
    width: 40px;
  }
  .header .image-text {
    display: flex;
    align-items: center;
  }
  .image {
    min-width: 60px;
    display: flex;
    align-items: center;
  }
  .header-text .name {
    font-weight: bold;
    font-size: 16px;
    transition: all 0.5s ease;
    white-space: nowrap;
  }
  .header .toggle {
    height: 40px;
    width: 40px;
    margin-top: 10px;
    border-radius: 50%;
    padding: 6px;
    cursor: pointer;
    .icon {
      width: 100%;
      height: 100%;
    }
    &:hover {
      background-color: var(--light-gray);
      transition: all 0.5s ease;
    }
  }
  li {
    height: 50px;
    margin-top: 20px;
    list-style: none;
    display: flex;
    align-items: center;
    transition: all 0.5s ease;
    .link {
      width: 100%;
      height: 100%;
    }
  }
  .menu-bar {
    height: calc(100% - 50px);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
  }
}

.close {
  width: 80px;
}
</style>
