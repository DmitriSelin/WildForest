<!-- eslint-disable vue/multi-word-component-names -->
<script setup>
import Checkbox from '@/components/checkboxes/WFCheckbox.vue'
import CheckboxGroup from '@/components/checkboxes/WFCheckboxGroup.vue'
import WFRadiobutton from '@/components/radiobuttons/WFRadiobutton.vue'
import WFProgressbar from '@/components/progresses/WFProgressbar.vue'
import WFButton from '@/components/buttons/WFButton.vue'
import WfCircleProgressbar from '@/components/progresses/WFCircleProgressbar.vue'
import WFTabs from '@/components/tabs/WFTabs.vue'
import { ref } from 'vue'

const isChecked = ref(true)

const switchOn = ref(false)

const listOfProducts = ref([
  { name: 'Bread', id: 'h1' },
  { name: 'Milk', id: 'h2' },
  { name: 'Eggs', id: 'h3' },
  { name: 'Cheese', id: 'h4' }
])

const listOfProducts2 = ref([
  { name: 'Bread', id: 'a1' },
  { name: 'Milk', id: 'a2' },
  { name: 'Eggs', id: 'a3' },
  { name: 'Cheese', id: 'a4' }
])

const selectedItems = ref(['h1', 'h3'])
const selectedItem = ref('')
const disabledRadio = ref(true)
const isDisabled = ref(true)
const percentBar = ref(30)

const addPercentbar = () => {
  if (percentBar.value >= 100) return

  percentBar.value += 10
}

const tabs = [
  { name: 'dotnet', label: '.NET' },
  { name: 'vue', label: 'Vue' },
  { name: 'postgres', label: 'PostgreSQL' }
]
const selectedTab = ref('vue')

const changeTab = (tabName) => {
  selectedTab.value = tabName
}
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
  <br />
  <br />
  <h1>Switch: {{ switchOn }}</h1>
  <Checkbox
    label="Dark theme"
    id="switch1"
    type="switch"
    name="switch1"
    value="theme1"
    v-model:checked="switchOn"
  />

  <br />
  <br />
  <h1>Radiobuttons</h1>
  <h1>Selected item: {{ selectedItem }}</h1>
  <div v-for="item in listOfProducts2" :key="item.id">
    <WFRadiobutton
      :value="item.name"
      :label="item.name"
      :id="item.id"
      name="itemGroup"
      v-model:checkedValue="selectedItem"
    />
  </div>
  <br /><br />
  <div>
    <WFRadiobutton
      value="Disabled"
      label="Disabled"
      id="Disabled"
      name="Disabled"
      v-model:checkedValue="selectedItem"
      :disabled="disabledRadio"
    />
  </div>
  <div>
    <WFRadiobutton
    value="Disabled2"
    label="Disabled 2"
    id="Disabled2"
    name="Disabled"
    v-model:checkedValue="selectedItem"
    :disabled="disabledRadio"
  />
  </div>

  <br /><br />
  <h2>Progress bar</h2>
  <WFProgressbar :percent="percentBar" maxWidth="100%" />
  <WFButton label="Add 10%" @click="addPercentbar" />

  <br /><br />
  <h2>Circle progress bar</h2>
  <WfCircleProgressbar :percent="percentBar" lineColor="success" />
  <br /><br />
  <WFTabs :names="tabs" :selectedTab="selectedTab" @changeTab="changeTab">
    <div v-if="selectedTab === 'dotnet'">About .NET</div>
    <div v-if="selectedTab === 'vue'">About Vue</div>
    <div v-if="selectedTab === 'postgres'">About postgres</div>
  </WFTabs>
</template>
