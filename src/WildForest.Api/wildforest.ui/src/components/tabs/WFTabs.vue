<script setup>
const props = defineProps({
  names: {
    type: Array,
    required: true
  },
  selectedTab: {
    type: String,
    required: false
  }
})

const emit = defineEmits(['changeTab'])

const clickOnTab = (tabName) => {
  emit('changeTab', tabName)
}
</script>

<template>
  <div class="tab-nav">
    <span
      v-for="tab in names"
      :key="tab.name"
      :class="['tab-nav__item', { selected: tab.name === selectedTab }]"
      @click="clickOnTab(tab.name)"
    >
      {{ tab.label }}
    </span>
  </div>
  <div class="tab-content">
    <slot></slot>
  </div>
</template>

<style lang="scss" scoped>
.tab {
  &-nav {
    display: flex;
    align-items: center;
    margin-bottom: 20px;
    color: var(--white);
    &__item {
      height: 40px;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-right: 10px;
      border-radius: 7px;
      cursor: pointer;
      border: 1px solid var(--accent);
      padding: 0 20px;
      font-size: 15px;
      &:hover {
        background: var(--accent-hover);
        color: var(--white);
        transition: 0.2s;
      }
      &.selected {
        background: var(--accent);
        color: var(--white);
      }
    }
  }
  &-content {
    padding: 20px;
    border-radius: 7px;
    background: var(--primary);
  }
}
</style>
