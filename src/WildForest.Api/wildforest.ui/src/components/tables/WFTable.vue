<script setup>
const props = defineProps({
  head: {
    type: Array,
    required: false
  },
  columnTemplates: {
    type: String,
    required: false
  }
})

const emit = defineEmits(['sorting'])

const clickOnHead = (name) => {
  emit('sorting', name.toLowerCase())
}
</script>

<template>
  <div class="table-wrapper">
    <div class="table">
      <div class="table-head" :style="{ 'grid-template-columns': columnTemplates }">
        <div
          class="table-head__name"
          v-for="(element, index) of head"
          :key="index"
          @click="clickOnHead(element)">
          {{ element }}
        </div>
      </div>
      <slot></slot>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.table {
  background-color: var(--light-gray);
  color: var(--white);
  width: 100%;
  margin-bottom: 40px;
  margin-top: 15px;
  &-wrapper {
    display: flex;
    justify-content: center;
  }
  &-head {
    padding: 5px 16px;
    color: var(--gray);
    display: grid;
    column-gap: 10px;
    align-items: center;
    border-bottom: 2px solid #eeeff4;
    background: var(--violet);
    &__name {
      display: flex;
      justify-content: flex-start;
      align-items: center;
      color: var(--white);
      cursor: pointer;
    }
    @media screen and (max-width: 767px) {
      display: none;
    }
  }
}
</style>
