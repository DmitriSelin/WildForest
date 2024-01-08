<script setup>
import getIconFromWeatherName from "./weatherIconUtils";

const props = defineProps({
    tabs: {
        type: Array,
        required: true
    },
    selectedTab: {
        type: String,
        required: true
    }
})
</script>

<template>
    <div class="tab-nav">
        <div v-for="tab in tabs" :key="tab.id" :class="['tab-nav__item', { 'selected': selectedTab === tab.time }]">
            <h3 class="tab-nav__item-h">{{ tab.time }}</h3>
            <font-awesome-icon :icon="`fa-solid fa-${getIconFromWeatherName(tab)}`" class="tab-nav__item-img" />
            <h3 class="tab-nav__item-h">{{ tab.temperature.value }}&nbsp;°C</h3>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.tab-nav {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-wrap: wrap;
    color: var(--white);
    width: 34vw;
    gap: 15px;
    margin: 15px 0;

    @media screen and (max-width: 600px) {
        width: 90vw;
    }

    &__item {
        color: var(--white);
        display: flex;
        flex-direction: column;
        flex-wrap: nowrap;
        align-items: center;
        justify-content: center;
        background-color: var(--violet);
        padding: 2px;
        height: 130px;
        width: 130px;
        gap: 10px;
        border-radius: 15px;
        cursor: pointer;

        @media screen and (max-width: 600px) {
            height: 85px;
            width: 85px;
        }

        &:hover {
            transform: scale(1.1);
            transition: 0.1s ease;
            box-shadow: 0px 0px 8px var(--white);
        }

        &.selected:not(:hover) {
            border: 2px solid var(--white);
        }

        &-h {
            margin: 0;
            @media screen and (max-width: 600px) {
                font-size: 12px;
            }
        }

        &-img {
            height: 50px;

            @media screen and (max-width: 600px) {
                height: 30px;
            }
        }
    }
}
</style>