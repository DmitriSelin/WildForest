<script setup>
import { ref } from 'vue';
const emit = defineEmits(['update:value']);
const props = defineProps({
    options: {
        type: Array,
        required: true
    },
    optionLabel: {
        type: String,
        default: 'name'
    },
    placeholder: {
        type: String,
        required: true
    },
    id: {
        type: String,
        required: true
    },
    value: {
        type: Object,
        required: true
    },
    editable: {
        type: Boolean,
        default: false
    },
    error: {
        type: String,
        required: false
    },
    isError: {
        type: Boolean,
        default: false
    },
    labelOnTop: {
        type: Boolean,
        default: false
    }
})

const selectedItem = ref();
const updateValue = () => {
    emit('update:value', selectedItem.value);
}
</script>

<template>
    <div class="dropdown-container">
        <Dropdown v-model="selectedItem" :options="options" :editable="editable" :optionLabel="optionLabel"
            :placeholder="placeholder" :id="id" @change="updateValue" />
        <Transition>
            <label :for="id" :class="['label', { 'label-top': labelOnTop === true }]" v-if="isError">{{ error }}</label>
        </Transition>
    </div>
</template>

<style lang="scss" scoped>
.dropdown-container {
    position: relative;
}

.label {
    position: absolute;
    color: var(--danger);
    font-size: 13px;
    bottom: -20px;
    left: 0;

    &-top {
        top: -20px;
        bottom: auto;
    }
}

.p-dropdown {
    background-color: var(--light-gray);
    color: var(--white);
    width: 300px;
    border: 0;
}

.v-enter-active,
.v-leave-active {
    transition: opacity 0.2s;
}

.v-enter-from,
.v-leave-to {
    opacity: 0;
}
</style>