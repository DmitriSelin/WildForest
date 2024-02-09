<script setup>
const props = defineProps({
    image: {
        type: String,
        default: "../../../assets/images/logo.ico"
    },
    size: {
        type: String,
        default: "large"
    },
    shape: {
        type: String,
        default: "circle"
    },
    alt: {
        type: String,
        default: "userImage"
    }
});
</script>

<template>
    <h1></h1>
</template>