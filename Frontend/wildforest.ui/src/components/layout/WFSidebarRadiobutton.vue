<script setup>
const emit = defineEmits(['update:checkedValue'])

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
  },
  checkedValue: {
    type: String,
    required: true
  }
})

const click = (event) => {
  emit('update:checkedValue', event.target.value)
}
</script>

<template>
  <input
    class="radio"
    type="radio"
    :name="name"
    :value="value"
    :id="id"
    :checked="checked"
    @input="click($event)"
  />
  <label :for="id">
    <font-awesome-icon :icon="`fa-solid fa-${icon}`" class="icon" />
    <span v-if="!isClosed">{{ label }}</span>
  </label>
</template>

<style scoped lang="scss">
.radio {
  z-index: -1;
  opacity: 0;
  & + label {
    cursor: pointer;
    font-weight: bold;
    display: flex;
    align-items: center;
    color: var(--white);
    display: flex;
    width: 100%;
    height: 100%;
    border-radius: 10px;
    .icon {
      height: 30px;
      margin-right: 20px;
      margin-left: 8px;
    }
  }
  &:hover:not(:checked) + label {
    background-color: var(--white);
    color: var(--second);
  }
  &:checked + label {
    background-color: var(--light-gray);
  }
}
</style>
