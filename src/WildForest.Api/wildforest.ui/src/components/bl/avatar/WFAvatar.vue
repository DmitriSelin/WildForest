<script setup>
const props = defineProps({
    image: {
        type: String,
        required: true,
        nullable: true
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
    <Avatar v-if="image !== null" :image="'data:image;base64,' + image" :size="size" :shape="shape" :ariaLabel="alt" />
    <img v-else src="../../../assets/images/logo.ico" :alt="alt" />
</template>