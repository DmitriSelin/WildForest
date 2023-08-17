<script setup>
import WFCheckbox from '@/components/checkboxes/WFCheckbox.vue';
const emit = defineEmits(['update:values']);

const props = defineProps({
  name: {
    type: String,
    required: true
  },
  values: {
    type: Array,
    required: true
  },
  options: {
    type: Array,
    required: true,
    validator: (value) => {
      const hasNameKey = value.every((option) => Object.keys(option).includes('name'));
      const hasIdKey = value.every((option) => Object.keys(option).includes('id'));
      return hasNameKey && hasIdKey;
    }
  }
});

const check = (params) => {
    let selectedValues = [...props.values];
    
    if (params.checked) {
      selectedValues.push(params.optionId);
    }
    else {
      selectedValues.splice(selectedValues.indexOf(params.optionId), 1);
    }

    emit('update:values', selectedValues);
}
</script>

<template>
  <div v-for="option in options" :key="option.id">
    <WFCheckbox
      :label="option.name"
      :id="option.id"
      :name="name"
      :value="option.name"
      :checked="values.includes(option.id)"
      group
      @updateCheckboxGroup="check"
    />
  </div>
</template>

<style lang="scss" scoped></style>
