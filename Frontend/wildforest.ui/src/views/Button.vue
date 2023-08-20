<!-- eslint-disable vue/multi-word-component-names -->
<script setup>
import Checkbox from '@/components/checkboxes/WFCheckbox.vue'
import CheckboxGroup from '@/components/checkboxes/WFCheckboxGroup.vue'
import WFRadiobutton from '@/components/radiobuttons/WFRadiobutton.vue'
import WFProgressbar from '@/components/progresses/WFProgressbar.vue'
import WFButton from '@/components/buttons/WFButton.vue'
import WfCircleProgressbar from '@/components/progresses/WFCircleProgressbar.vue'
import WFTabs from '@/components/tabs/WFTabs.vue'
import WFTable from '@/components/tables/WFTable.vue'
import WFTableRow from '@/components/tables/WFTableRow.vue'
import WFTableColumn from '@/components/tables/WFTableColumn.vue'
import { ref } from 'vue'
import { computed } from 'vue'

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

//Tables

const tableHeads = ['Id', 'Author', 'Title', 'Cover', '']
const tableSizeColumns = '50px 1fr 2fr 150px 140px'

const books = ref([
  {
    id: 1,
    author: 'Dmitry Glukhovsky',
    title: 'Metro 2033',
    image: 'https://m.media-amazon.com/images/P/0735211299.01._SCLZZZZZZZ_SX500_.jpg',
    bg: '#FFA26B'
  },
  {
    id: 12,
    author: 'James Clear',
    title: 'Atomic Habits: An Easy',
    image: 'https://m.media-amazon.com/images/P/0735211299.01._SCLZZZZZZZ_SX500_.jpg',
    bg: '#00C48C'
  },
  {
    id: 2,
    author: 'J. K. Rowling',
    title: 'Harry Potter and the Order of the Phoenix',
    image: 'https://m.media-amazon.com/images/P/0735211299.01._SCLZZZZZZZ_SX500_.jpg',
    bg: '#00C48C'
  }
])

const sortField = ref('id')
const typeSort = ref('asc')

const booksSorting = computed(() => {
  return books.value.sort((a, b) => {
    let modifier = 1
    if (typeSort.value === 'desc') modifier = -1
    if (a[sortField.value] < b[sortField.value]) return -1 * modifier
    if (a[sortField.value] > b[sortField.value]) return 1 * modifier
    return 0
  })
})

const setSort = (name) => {
  if (sortField.value === name) {
    if (typeSort.value === 'asc') {
      typeSort.value = 'desc'
    } else {
      typeSort.value = 'asc'
    }
  } else {
    sortField.value = name
    setSort(name);
  }
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
  <h1>Tabs</h1>
  <WFTabs :names="tabs" :selectedTab="selectedTab" @changeTab="changeTab">
    <div v-if="selectedTab === 'dotnet'">About .NET</div>
    <div v-if="selectedTab === 'vue'">About Vue</div>
    <div v-if="selectedTab === 'postgres'">About postgres</div>
  </WFTabs>

  <br /><br /><br /><br />
  <h1>Table</h1>
  <h2>Sort field: {{ sortField }}</h2>
  <br />
  <WFTable :head="tableHeads" :columnTemplates="tableSizeColumns" @sorting="setSort">
    <WFTableRow
      v-for="book in booksSorting"
      :key="book.id"
      :columnTemplates="tableSizeColumns"
      :bgRow="book.bg"
    >
      <WFTableColumn :columnTitle="tableHeads[0]">
        {{ book.id }}
      </WFTableColumn>
      <WFTableColumn :columnTitle="tableHeads[1]">
        {{ book.author }}
      </WFTableColumn>
      <WFTableColumn :columnTitle="tableHeads[2]">
        {{ book.title }}
      </WFTableColumn>
      <WFTableColumn :image="true" :srcImage="book.image" />
      <WFTableColumn>
        <WFButton label="Read" />
      </WFTableColumn>
    </WFTableRow>
  </WFTable>
</template>
