<script setup>
import WFButton from "@/components/buttons/WFButton.vue"
import { ref } from "vue";
import { useUserStore } from "@/stores/UserStore";

const userStore = useUserStore();

const selectedCity = ref();
const cities = ref([
    { name: 'New York', code: 'NY' },
    { name: 'Rome', code: 'RM' },
    { name: 'London', code: 'LDN' },
    { name: 'London1', code: 'LDN1' },
    { name: 'London2', code: 'LDN2' },
    { name: 'London3', code: 'LDN3' },
    { name: 'London4', code: 'LDN4' },
    { name: 'London5', code: 'LDN5' },
    { name: 'London7', code: 'LDN7' },
    { name: 'London8', code: 'LDN8' },
    { name: 'London6', code: 'LDN6' },
    { name: 'Istanbul', code: 'IST' },
    { name: 'Paris', code: 'PRS' }
]);
</script>

<template>
    <main class="main">
        <div class="left-block">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Registration</h1>
            </div>
            <form class="left-block-content">
                <Dropdown v-model="selectedCity" editable :options="userStore.registerCredentials.languages"
                    optionLabel="name" placeholder="Select language" class="combobox" />
                <Dropdown v-model="selectedCity" editable :options="userStore.registerCredentials.countries" optionLabel="name" placeholder="Select a City"
                    class="combobox" />
                <WFButton label="Next" size="large" />
            </form>
        </div>
        <div class="right-block">
            <img src="../assets/images/appLogo.svg" alt="logo" class="right-block-image">
            <h1 class="right-block-name">Wild forest</h1>
        </div>
    </main>
</template>

<style lang="scss" scoped>
@import "./styles/views.scss";
@import "../components/styles/combobox.scss";
</style>