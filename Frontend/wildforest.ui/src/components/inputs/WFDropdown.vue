<script setup>
import { ref } from 'vue';
const emit = defineEmits(['selectionChanged']);
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
    error: {
        type: String,
        required: false
    },
    isError: {
        type: Boolean,
        default: false
    }
})

const selectedItem = ref();
const dataChanged = () => {
    emit('selectionChanged', selectedItem.value);
}
</script>

<template class="dropdown-container">
    <div class="dropdown-container">
        <Dropdown v-model="selectedItem" :options="options" :optionLabel="optionLabel" :placeholder="placeholder" :id="id"
            @change="dataChanged" />
        <Transition v-if="isError">
            <label :for="id" class="label">{{ error }}</label>
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
    left: 5px;
}

.p-dropdown {
    background-color: var(--light-gray);
    color: var(--white);
    width: 300px;
}

.v-enter-active,
.v-leave-active {
    transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
    opacity: 0;
}
</style>