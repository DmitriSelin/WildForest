<script setup>
import WFRadiobutton from "./WFRadiobutton.vue";
import { getItemFromSessionStorage, setItemInSessionStorage } from "@/infrastructure/storage/storageUtils";
import { ref, onMounted, watch } from 'vue';

const up = '1';
const down = '2';
const upButtonCheckedStorageName = 'upBtn';

const props = defineProps({
    rating: {
        type: Number,
        required: true
    },
    selectedValue: {
        type: Number,
        required: true
    },
    views: {
        type: String,
        required: false
    }
});

const upButtonChecked = ref(null);

onMounted(() => {
    try {
        upButtonChecked.value = getItemFromSessionStorage(upButtonCheckedStorageName);
    }
    catch (err) { /* empty */ }
});

const emits = defineEmits(['up', 'down']);
const vote = ref('');

watch(() => vote.value, (state) => {
    if (state === up) {
        setItemInSessionStorage(upButtonCheckedStorageName, true);
        emits('up');
    }
    else if (state === down) {
        setItemInSessionStorage(upButtonCheckedStorageName, false);
        emits('down');
    }
});
</script>

<template>
    <div class="container">
        <WFRadiobutton name="vote" id="up" value="1" v-model:checkedValue="vote" icon="up"
            :checked="upButtonChecked === true || selectedValue === 1" />
        <span class="count">{{ rating }}</span>
        <WFRadiobutton name="vote" id="down" value="2" v-model:checkedValue="vote" icon="down"
            :checked="upButtonChecked === false || selectedValue === 2" />
    </div>
</template>

<style lang="scss" scoped>
.container {
    display: flex;

    .count {
        padding: 5px 10px;
        border: solid var(--gray) 2px;
        border-radius: 5px;
        margin: 0 20px;
    }
}
</style>