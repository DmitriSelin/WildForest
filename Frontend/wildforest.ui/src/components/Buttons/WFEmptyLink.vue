<script setup>
const props = defineProps({
    to: {
        type: String,
        required: true
    },
    text: {
        type: String,
        required: true
    },
    title: {
        type: String,
        default: ''
    }
})
</script>

<template>
    <router-link :to="to">
        <span class="text">{{ text }}</span>
        <span class="title">
            &nbsp;
            {{' ' + title }}
        </span>
    </router-link>
</template>

<style lang="scss" scoped>
a {
    display: flex;
    flex-direction: row;
}

.text {
    font-size: 15px;
    color: var(--gray);
}

.title {
    font-size: 15px;
    color: var(--white);
}
</style>