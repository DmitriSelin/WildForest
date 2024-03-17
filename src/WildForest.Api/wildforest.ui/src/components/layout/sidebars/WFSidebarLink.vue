<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { collapsed } from './state'

const props = defineProps({
    to: {
        type: String,
        required: true
    },
    icon: {
        type: String,
        required: true
    }
})

const route = useRoute()
const isActive = computed(() => route.path == props.to)
</script>

<template>
    <router-link :to="to" class="link" :class="{ active: isActive }">
        <font-awesome-icon :icon="`fa-solid fa-${icon}`" class="icon" />
        <Transition name="fade">
            <span v-if="!collapsed">
                <slot></slot>
            </span>
        </Transition>
    </router-link>
</template>

<style lang="scss" scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.1s;
}

.fade-enter,
.fade-leave-to {
    opacity: 0;
}

.link {
    display: flex;
    align-items: center;
    cursor: pointer;
    position: relative;
    text-decoration: none;
    color: var(--white);
    border-radius: 0.25em;
    height: 45px;
    margin: 5px;
    font-weight: bold;

    &:hover:not(--isActive) {
        background-color: var(--white);
        color: var(--second);
    }

    &.active {
        background-color: var(--light-gray);

        &:hover {
            background-color: var(--light-gray);
            color: var(--white);
        }
    }

    .icon {
        flex-shrink: 0;
        height: 25px;
        margin-right: 15px;
        margin-left: 8.5px;
    }
}
</style>