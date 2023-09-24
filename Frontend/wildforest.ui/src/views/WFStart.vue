<script setup>
import WFButton from '@/components/buttons/WFButton.vue'
</script>

<template>
    <div class="background">
        <img src="../assets/images/appLogo.svg" />
        <h1 class="background-title">Wild forest</h1>
        <WFButton label="Login" outlined size="large" />
        <WFButton label="Start without login" outlined size="large" />
    </div>
</template>

<style lang="scss" scoped>
.background {
    background-image: url(../assets/images/background.jpg);
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 70px;

    &-title {
        color: transparent;
        font-size: 60px;
        font-weight: bold;
        -webkit-text-stroke: 1px var(--white);
    }
}
</style>