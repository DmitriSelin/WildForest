<!-- eslint-disable vue/multi-word-component-names -->
<script setup>
import Checkbox from '@/components/checkboxes/WFCheckbox.vue'
import CheckboxGroup from '@/components/checkboxes/WFCheckboxGroup.vue'
import { ref } from 'vue'

const isChecked = ref(true)

const listOfProducts = ref([
  { name: 'Bread', id: 'h1' },
  { name: 'Milk', id: 'h2' },
  { name: 'Eggs', id: 'h3' },
  { name: 'Cheese', id: 'h4' }
])

const selectedItems = ref(['h1', 'h3'])
</script>

<template>
  <h1>{{ isChecked }}</h1>
  <Checkbox
    label="Go to top"
    id="checkboxActive"
    name="checkboxActive"
    value="like"
    v-model:checked="isChecked"
  />

  <br />
  <br />
  <h1>Selected products: {{ selectedItems }}</h1>
  <CheckboxGroup v-model:values="selectedItems" name="myProducts" :options="listOfProducts" />
</template>
