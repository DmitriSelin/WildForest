<script setup>
const props = defineProps({
  links: {
    type: Array,
    required: true
  }
})
</script>

<template>
  <nav class="sidebar">
    <div class="header">
      <div class="image-text">
        <span class="image">
          <img src="../../assets/images/logo.ico" alt="logo">
        </span>
        <div class="text header-text">
          <span class="name">Wild forest</span>
        </div>
      </div>
      <div class="toggle">
        <font-awesome-icon icon="fa-solid fa-bars" />
      </div>
    </div>
    <div class="menu-bar">
      <div class="menu">
        <ul class="menu-links">
          <li v-for="link in links" :key="link.url">
            <router-link></router-link>
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
    position: relative;
  }
  .image-text img {
    width: 40px;
  }
  .header .image-text {
    display: flex;
    align-items: center;
  }
  .text {
    font-size: 16px;
    font-weight: 500;
  }
  .image {
    min-width: 60px;
    display: flex;
    align-items: center;
  }
  .header-text .name {
    font-weight: bold;
  }
  .header .toggle {
    position: absolute;
    margin: 20px auto 10px 3px;
    height: 35px;
    width: 35px;
    display: flex;
    align-items: center;
    align-content: center;
    border-radius: 50%;
    padding: 4px;
    cursor: pointer;
    .fa-bars {
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
    margin-top: 10px;
    list-style: none;
  }
}
</style>
