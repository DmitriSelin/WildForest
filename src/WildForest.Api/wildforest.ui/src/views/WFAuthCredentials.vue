<script setup>
import WFButton from "@/components/buttons/WFButton.vue"
import WFDropdown from "@/components/inputs/WFDropdown.vue";
import { ref, onMounted } from "vue";
import { validateNotEmptyValues } from "@/auth/validators/fieldValidator";
import { useUserStore } from "@/stores/UserStore";
import { getAuthCredentialsFormData } from "@/infrastructure/formProvider";
import { goTo } from "@/api/api";

const formData = ref(getAuthCredentialsFormData());
const userStore = useUserStore();
const errors = ref([]);

onMounted(async () => {
    await userStore.getAuthCredentials();
});

const goToRegisterPage = () => {
    const validationResult = validateNotEmptyValues([formData.value.selectedLanguage, formData.value.selectedCountry]);

    if (validationResult.isValid) {
        userStore.setAuthCredentials(formData.value.selectedLanguage, formData.value.selectedCountry);
        goTo("Registration");
    }
    else {
        errors.value = validationResult.errors;
    }
}
</script>

<template>
    <main class="main">
        <div class="left-block small-gap">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Registration</h1>
            </div>
            <form class="left-block-content small-area" @submit.prevent="goToRegisterPage">
                <WFDropdown :options="userStore.authCredentials.languages" placeholder="Select a language"
                    id="languageDropdown" error="This field is required" v-model:value="formData.selectedLanguage"
                    :isError="errors[0] === true" />
                <WFDropdown :options="userStore.authCredentials.countries" placeholder="Select a country"
                    id="countryDropdown" error="This field is required" v-model:value="formData.selectedCountry"
                    :isError="errors[1] === true" />
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

.small-gap {
    gap: 10vh;

    @media screen and (max-width: 600px) {
        gap: 3vh;
    }
}
</style>