<script setup>
const props = defineProps({
    label: {
        type: String,
        default: 'Button'
    },
    to: {
        type: String,
        required: true
    },
    color: {
        type: String,
        default: 'accent'
    },
    rounded: {
        type: Boolean,
        required: false
    },
    outlined: {
        type: Boolean,
        required: false
    },
    size: {
        type: String,
        default: 'normal'
    }
})
</script>

<template>
    <router-link :to="to" :class="[
        'btn',
        `btn_${color}`,
        { btn_rounded: rounded },
        { btn_outlined: outlined },
        { btn_large: size === 'large' }]">
        {{ label }}
    </router-link>
</template>

<style lang="scss" scoped>
.btn {
    margin-right: 10px;
    color: var(--white);
    font-weight: bold;
    cursor: pointer;
    padding: 5px 20px;
    transition: 0.2s;
    border-radius: 7px;
    font-size: 15px;

    &_accent {
        background-color: var(--accent);
        border: 1px solid var(--accent);

        &:hover {
            background-color: var(--accent-hover);
        }
    }

    &_success {
        background-color: var(--success);
        border: 1px solid var(--success);

        &:hover {
            background-color: var(--success-hover);
        }
    }

    &_warning {
        background-color: var(--warning);
        border: 1px solid var(--warning);

        &:hover {
            background-color: var(--warning-hover);
        }
    }

    &_danger {
        background-color: var(--danger);
        border: 1px solid var(--danger);

        &:hover {
            background-color: var(--danger-hover);
        }
    }

    &_rounded {
        border-radius: 15px;
    }

    &_outlined {
        background-color: transparent;
        color: var(--white);
    }

    &_large {
        height: 48px;
        padding: 0 30px;
        padding-top: 15px;
    }
}
</style>
