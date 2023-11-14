<script setup>
const props = defineProps({
    title: {
        type: String,
        required: true
    },
    value: {
        type: String,
        required: true
    },
    icon: {
        type: String,
        required: true
    }
});
</script>

<template>
    <div class="content">
        <font-awesome-icon :icon="`fa-solid fa-${icon}`" class="content-icon"/>
        <div class="data">
            <span class="text">{{ title }}</span>
            <span class="text">{{ value }}</span>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.content {
    background-color: var(--violet);
    border-radius: 5px;
    width: 180px;
    height: 70px;
    margin: 1vh;
    padding: 5px 15px;
    display: flex;
    justify-content: space-between;
    align-items: center;

    &:hover {
        transform: scale(1.1);
        transition: 0.1s ease;
        box-shadow: 0px 0px 8px var(--white);
    }

    &-icon {
        height: 50px;
    }
}
</style>