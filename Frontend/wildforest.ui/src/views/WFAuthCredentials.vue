<script setup>
import WFButton from "@/components/buttons/WFButton.vue"
import WFDropdown from "@/components/inputs/WFDropdown.vue";
import { ref, onMounted } from "vue";
import { useUserStore } from "@/stores/UserStore";

const selectedLanguage = ref();
const selectedCountry = ref();
const userStore = useUserStore();
const t = ref(true);

onMounted(async () => {
    await userStore.getAuthCredentials();
});

const goToRegisterPage = () => {
    t.value = !t.value;
}

const selectionChanged1 = (data) => {

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
                <WFDropdown :options="userStore.authCredentials.languages" placeholder="Select a language"
                    id="languageDropdown" />
                <WFDropdown :options="userStore.authCredentials.countries" placeholder="Select a country"
                    id="countryDropdown" />
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

.small-area {
    gap: 10vh;
}
</style>