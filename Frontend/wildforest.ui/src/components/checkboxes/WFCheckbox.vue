<script setup>
const props = defineProps({
  name: {
    type: String,
    default: ''
  },
  id: {
    type: String,
    default: ''
  },
  value: {
    type: String,
    default: ''
  },
  label: {
    type: String,
    default: ''
  }
})
</script>

<template>
  <input class="checkbox" type="checkbox" name="" id="" value="" />
  <label for="id">{{ label }}</label>
</template>

<style lang="scss" scoped></style>
