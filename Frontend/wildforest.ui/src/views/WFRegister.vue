<script setup>
import WFInput from "@/components/inputs/WFInput.vue";
import WFDropdown from "@/components/inputs/WFDropdown.vue";
import WFButton from "@/components/buttons/WFButton.vue";
import WFEmptyLink from "@/components/buttons/WFEmptyLink.vue";
import { validateSameFields, validateNotEmptyValue } from "@/auth/validators/fieldValidator";
import { ref, onMounted } from "vue";
import { useUserStore } from "@/stores/UserStore"
import { useToast } from "primevue/usetoast";
import { useRouter } from "vue-router";

const toast = useToast();
const userStore = useUserStore();
const router = useRouter();

onMounted(async () => {
    await userStore.getCitiesByCountry();
});

const firstName = ref("");
const lastName = ref("");
const email = ref("");
let selectedCity;
const password = ref("");
const samePassword = ref("");
const errors = ref([]);

const register = async () => {
    let comboboxValidationResult = validateNotEmptyValue(selectedCity);
    let result = validateSameFields(password.value, samePassword.value);

    if (result.isValid === true && comboboxValidationResult.isValid === true) {
        const result = await userStore.register({
            firstName: firstName.value, lastName: lastName.value,
            email: email.value, password: password.value, cityId: selectedCity.cityId, languageId: ""
        });

        if (result === true) {
            router.push({ name: 'Weather' });
        }
        else if (result === false) {
            toast.add({ severity: 'error', summary: 'Error', detail: userStore.errorMessage, life: 10000 });
        }
    }
    else {
        if (comboboxValidationResult.isValid === false) {
            errors.value[0] = true;
            return;
        }
        else if (result.isValid === false) {
            errors.value[0] = false;
            errors.value[1] = true;
            return;
        }
    }
}

const citySelectionChanged = (city) => {
    selectedCity = city;
}

const registerWithGoogle = () => {
    toast.add({ severity: 'info', summary: 'Info', detail: 'This function is still in development', life: 3000 });
}
</script>

<template>
    <main class="main">
        <div class="left-block">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Registration</h1>
            </div>
            <form @submit.prevent="register" class="left-block-content">
                <WFInput label="Firstname" name="firstName" placeholder="Input your first name" v-model:value="firstName" />
                <WFInput label="Lastname" name="lastName" placeholder="Input your lastname" v-model:value="lastName" />
                <WFInput label="Email" type="email" name="email" placeholder="Input your email" v-model:value="email" />
                <WFDropdown :options="userStore.cities" placeholder="Select a City" id="cityDropdown"
                    error="This field is required" :isError="errors[0] === true" optionLabel="cityName" editable
                    @selectionChanged="citySelectionChanged" :labelOnTop="true" />
                <WFInput label="Password" type="password" name="password" placeholder="Input your password"
                    v-model:value="password" minLength="6" error="Input the same passwords" :isError="errors[1] === true" />
                <WFInput label="Password" type="password" name="samePassword" placeholder="Input the same password"
                    v-model:value="samePassword" minLength="6" error="Input the same passwords"
                    :isError="errors[1] === true" />
                <div class="left-block-content-btn">
                    <WFButton label="Register" size="large" />
                    <WFEmptyLink to="login" text="Already have an account?" title="Login" />
                </div>
                <span class="left-block-content-txt">or</span>
                <Toast />
                <WFButton iconPackName="brands" icon="google" @click="registerWithGoogle" type="button" />
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
</style>