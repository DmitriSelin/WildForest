<script setup>
import WFButton from "@/components/buttons/WFButton.vue"
import { ref, onMounted } from "vue";
import { useUserStore } from "@/stores/UserStore";

const selectedLanguage = ref();
const selectedCountry = ref();
const userStore = useUserStore();

onMounted(async () => {
    await userStore.getAuthCredentials();
});

const goToRegisterPage = () => {
    
}
</script>

<template>
    <main class="main">
        <div class="left-block small-area">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Registration</h1>
            </div>
            <form class="left-block-content small-area" @submit.prevent="goToRegisterPage">
                <Dropdown v-model="selectedLanguage" :options="userStore.authCredentials.languages" optionLabel="name"
                    placeholder="Select a language"/>
                <Dropdown v-model="selectedCountry" :options="userStore.authCredentials.countries" optionLabel="name"
                    placeholder="Select a country"/>
                <WFButton label="Next" size="large" @click="goToRegisterPage"/>
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

.small-area {
    gap: 10vh;
}
</style>