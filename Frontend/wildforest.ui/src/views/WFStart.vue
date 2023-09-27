<script setup>
import WFButtonLink from '@/components/buttons/WFButtonLink.vue'
</script>

<template>
    <div class="background">
        <img src="../assets/images/appLogo.svg" />
        <h1 class="background-title">Wild forest</h1>
        <WFButtonLink label="Login" outlined size="large" to="/auth/login" />
        <WFButtonLink label="Start without login" outlined size="large" to="/" />
    </div>
</template>

<style lang="scss" scoped>
.background {
    background-image: url(../assets/images/background.jpg);
    background-repeat: no-repeat;
    background-size: cover;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 70px;
    overflow-x: auto;

    &-title {
        color: transparent;
        font-size: 60px;
        font-weight: bold;
        -webkit-text-stroke: 1px var(--white);
    }
}
</style>