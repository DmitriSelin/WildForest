<script setup>
const props = defineProps({
    label: {
        type: String,
        default: "Button"
    }
});
</script>

<template>
    <button class="btn">{{ label }}</button>
</template>

<style scoped>
.btn {
    background-color: var(--blue);
    color: var(--white);
}
</style>