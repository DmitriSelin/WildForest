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
  },
  checked: {
    type: Boolean,
    default: false
  },
  icon: {
    type: String,
    required: false
  },
  isClosed: {
    type: Boolean,
    required: true
  }
})
</script>

<template>
  <div class="radio-option" :class="{'center': isClosed}">
    <input type="radio" :name="name" :value="value" :id="id" class="input" />
    <label :for="id">
      <font-awesome-icon :icon="`fa-solid fa-${icon}`" class="icon" />
      <span v-if="!isClosed">{{ label }}</span>
    </label>
  </div>
</template>

<style scoped lang="scss">
.radio-option {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
  cursor: pointer;
  padding: 9px;
  background-color: green;
  width: 100%;
  height: 100%;
  border-radius: 10px;
  .input {
    display: none;
  }
}
label {
  cursor: pointer;
  font-weight: bold;
  display: flex;
  align-items: center;
  .icon {
    height: 30px;
    margin-right: 15px;
  }
}

.center {
  padding: 0 auto
}
</style>
