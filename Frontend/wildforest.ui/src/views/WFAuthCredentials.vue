<script setup>
import WFButton from "@/components/buttons/WFButton.vue"
import WFDropdown from "@/components/inputs/WFDropdown.vue";
import { ref, onMounted } from "vue";
import { validate } from "@/auth/validators/comboboxValidator";
import { useUserStore } from "@/stores/UserStore";
import { useRouter } from "vue-router";

let selectedLanguage;
let selectedCountry;
const userStore = useUserStore();
const router = useRouter();
const errors = ref([]);

onMounted(async () => {
    await userStore.getAuthCredentials();
});

const goToRegisterPage = () => {
    const validationResult = validate(2, [selectedLanguage, selectedCountry]);

    if (validationResult.isValid) {
        userStore.setAuthCredentials(selectedCountry, selectedLanguage);
        router.push({ name: 'Registration' });
    }
    else {
        errors.value = validationResult.errors;
    }
}

const languageSelectionChanged = (language) => {
    selectedLanguage = language;
}
const countrySelectionChanged = (country) => {
    selectedCountry = country;
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
                    id="languageDropdown" @selectionChanged="languageSelectionChanged" error="This field is required"
                    :isError="errors[0] === false" />
                <WFDropdown :options="userStore.authCredentials.countries" placeholder="Select a country"
                    id="countryDropdown" @selectionChanged="countrySelectionChanged" error="This field is required"
                    :isError="errors[1] === false" />
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