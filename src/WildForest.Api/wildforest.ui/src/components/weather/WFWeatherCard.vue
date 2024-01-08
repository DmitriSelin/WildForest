<script setup>
const props = defineProps({
    title: {
        type: String,
        required: true
    },
    value: {
        type: Number,
        required: true
    },
    valueType: {
        type: String,
        default: '%'
    },
    icon: {
        type: String,
        required: true
    }
});
</script>

<template>
    <div class="content">
        <font-awesome-icon :icon="`fa-solid fa-${icon}`" class="icon" />
        <div class="data">
            <h3 class="text">{{ title }}</h3>
            <h3 class="text">{{ value }}&nbsp;{{ valueType }}</h3>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.content {
    background-color: var(--violet);
    border-radius: 5px;
    width: 190px;
    height: 80px;
    margin: 1vh;
    padding: 5px 15px;
    display: flex;
    gap: 20px;
    justify-content: space-between;
    align-items: center;

    @media screen and (max-width: 600px) {
        width: 100px;
        height: 50px;
        padding: 3px;
        gap: 8px;
        justify-content: space-around;
    }

    &:hover {
        transform: scale(1.1);
        transition: 0.1s ease;
        box-shadow: 0px 0px 8px var(--white);
    }

    .icon {
        height: 40px;

        @media screen and (max-width: 600px) {
            height: 20px;
        }
    }

    h3 {
        margin: 0;
        text-wrap: wrap;

        @media screen and (max-width: 600px) {
            font-size: 12px;
        }
    }
}
</style>