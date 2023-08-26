<script setup>
import { ref } from 'vue'

const props = defineProps({
  links: {
    type: Array,
    required: true
  },
  selectedBar: {
    type: String,
    required: false
  }
})

const emit = defineEmits(['changeBar'])

const clickOnBar = (label) => {
  emit('changeBar', label)
}

const isClosed = ref(true)

const changeSidebarSize = () => {
  isClosed.value = !isClosed.value
}
</script>

<template>
  <nav class="sidebar" :class="{ close: isClosed }">
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
      <ul>
        <li v-for="link in links" :key="link.id" @click="clickOnBar(link.label)">
          <router-link :to="link.href" :class="['link', {'selected': link.label === selectedBar}]">
            <font-awesome-icon :icon="`fa-solid fa-${link.icon}`" class="icon" />
            <span v-if="!isClosed">{{ link.label }}</span>
          </router-link>
        </li>
      </ul>
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
    transition: all 0.5s ease;
  }
  .image {
    min-width: 60px;
    display: flex;
    align-items: center;
  }
  .header-text .name {
    font-weight: bold;
    font-size: 16px;
    white-space: nowrap;
    color: var(--white);
  }
  .header .toggle {
    height: 43px;
    width: 43px;
    margin-top: 10px;
    margin-bottom: 10px;
    border-radius: 50%;
    padding: 6px;
    cursor: pointer;
    .icon {
      width: 100%;
      height: 100%;
      color: var(--white);
    }
    &:hover {
      background-color: var(--light-gray);
      transition: all 0.5s ease;
    }
  }
  li {
    height: 50px;
    list-style: none;
    margin-bottom: 15px;
    display: flex;
    align-items: center;
    transition: all 0.5s ease;
    .link {
      width: 100%;
      height: 100%;
      cursor: pointer;
      font-weight: bold;
      display: flex;
      align-items: center;
      color: var(--white);
      display: flex;
      width: 100%;
      height: 100%;
      border-radius: 10px;
      &:hover {
        background-color: var(--white);
        color: var(--second);
      }
      .icon {
        height: 30px;
        margin-right: 20px;
        margin-left: 8px;
      }
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

.selected {
  background-color: var(--light-gray);
}
</style>
