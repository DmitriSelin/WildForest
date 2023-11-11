<script setup>
import WFRadiobutton from './WFRadiobutton.vue';
import { ref } from 'vue';

const props = defineProps({
    rating: {
        type: String,
        required: true
    },
    views: {
        type: String,
        required: false
    }
});

const vote = ref('');
</script>

<template>
    <div class="container">
        <WFRadiobutton name="vote" id="up" value="1" v-model:checkedValue="vote" icon="up"/>
        <span class="count">{{ rating }}</span>
        <WFRadiobutton name="vote" id="down" value="2" v-model:checkedValue="vote" icon="down"/>
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