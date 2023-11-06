<script setup>
import WFButton from "@/components/buttons/WFButton.vue";
</script>

<template>
    <div class="container">
        <img src="../assets/images/appLogo.svg">
        <h1>Page not found</h1>
        <p>Sorry, the page you're looking for does not exist</p>
        <WFButton label="Go to start page" size="large" @click="$router.push({name: 'Start'})" style="margin-top: 5vh;"/>
    </div>
</template>

<style lang="scss" scoped>
.container {
    height: 100vh;
    display: flex;
    flex-direction: column;
    flex-wrap: nowrap;
    justify-content: center;
    align-items: center;
    gap: 2vh;
}

p {
    white-space: wrap;
    padding: 0 20px;
    font-size: 18px;
    text-align: center;
}
</style>