<script setup>
const props = defineProps({
    tabs: {
        type: Array,
        required: true
    },
    selectedTab: {
        type: String,
        required: false
    }
})
</script>

<template>
    <div class="tab-nav">
        <div v-for="tab in tabs" :key="tab.id" class="tab-nav__item">
            <h3 class="tab-nav__item-h">{{ tab.time }}</h3>
            <font-awesome-icon icon="fa-solid fa-cloud-rain" class="tab-nav__item-img" />
            <h3 class="tab-nav__item-h">{{ tab.temperature }}</h3>
        </div>
    </div>
    <div class="tab-content">
        <slot></slot>
    </div>
</template>

<style lang="scss" scoped>
.tab-nav {
    display: flex;
    align-items: center;
    color: var(--white);
    gap: 15px;

    &__item {
        color: var(--white);
        display: flex;
        flex-direction: column;
        flex-wrap: nowrap;
        align-items: center;
        justify-content: center;
        background-color: var(--violet);
        height: 130px;
        width: 130px;
        gap: 10px;
        border-radius: 15px;
        cursor: pointer;

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
        }

        &-img {
            height: 50px;
        }
    }
}

.tab-content {
    height: 100vh;
}
</style>