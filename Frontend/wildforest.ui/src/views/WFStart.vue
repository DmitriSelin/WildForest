<script setup>
import WFButton from '@/components/buttons/WFButton.vue'

const goToLoginView = () => {
    window.location.href = "http://localhost:5173/buttons";
}

const goToMainView = () => {
    window.location.href = "http://localhost:5173";
}
</script>

<template>
    <div class="background">
        <img src="../assets/images/appLogo.svg" />
        <h1 class="background-title">Wild forest</h1>
        <WFButton label="Login" outlined size="large" @click="goToLoginView" />
        <WFButton label="Start without login" outlined size="large" @click="goToMainView" />
    </div>
</template>

<style lang="scss" scoped>
.background {
    background-image: url(../assets/images/background.jpg);
    background-repeat: no-repeat;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 70px;
    -moz-background-size: 100%;
    -webkit-background-size: 100%;
    -o-background-size: 100%;
    background-size: 100%;

    &-title {
        color: transparent;
        font-size: 60px;
        font-weight: bold;
        -webkit-text-stroke: 1px var(--white);
    }
}
</style>