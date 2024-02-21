<script setup>
import WFInput from "@/components/inputs/WFInput.vue";
import WFDropdown from "@/components/inputs/WFDropdown.vue";
import WFButton from "@/components/buttons/WFButton.vue";
import WFEmptyLink from "@/components/buttons/WFEmptyLink.vue";
import { validateSameFields, validateNotEmptyValue } from "@/auth/validators/fieldValidator";
import { ref, onMounted } from "vue";
import { UserService } from "@/users/userService";
import { useToast } from "primevue/usetoast";
import { SUCCESS, ERROR } from "@/api/apiConstants";
import { ERROR_SEVERITY, STANDARD_LIFE } from "@/infrastructure/components/toasts/toastConstants";
import { getRegisterFormData } from "@/infrastructure/formProvider";
import { goTo } from "@/api/api";

const toast = useToast();
const userService = new UserService();
const cities = ref([]);
const formData = ref(getRegisterFormData());
const errors = ref([]);

onMounted(async () => {
    const requestResult = await userService.getCitiesByCountry();

    if (requestResult.result === SUCCESS)
        cities.value = requestResult.data;
    else if (requestResult.result === ERROR)
        toast.add({ severity: ERROR_SEVERITY, summary: 'Error', detail: requestResult.data.title, life: STANDARD_LIFE });
});

const register = async () => {
    let comboboxValidationResult = validateNotEmptyValue(formData.value.selectedCity.name);
    let validationResult = validateSameFields(formData.value.password, formData.value.samePassword);

    if (validationResult.isValid === true && comboboxValidationResult.isValid === true) {
        const request = createRequest();
        const requestResult = await userService.register(request);

        if (requestResult.result === SUCCESS) {
            goTo("Home");
        }
        else if (requestResult.result === ERROR) {
            toast.add({ severity: ERROR_SEVERITY, summary: 'Error', detail: requestResult.data.title, life: STANDARD_LIFE });
        }
    }
    else {
        if (comboboxValidationResult.isValid === false) {
            errors.value[0] = true;
            return;
        }
        else if (validationResult.isValid === false) {
            errors.value[0] = false;
            errors.value[1] = true;
            return;
        }
    }
}

function createRequest() {
    const request = {
        firstName: formData.value.firstName, lastName: formData.value.lastName,
        email: formData.value.email, password: formData.value.password,
        image: formData.value.image, cityId: formData.value.selectedCity.id, languageId: ""
    }

    return request;
}

const onImageSelect = async (event) => {
    const file = event.files[0];
    let blob = await fetch(file.objectURL).then((r) => r.blob());
    const reader = new FileReader();
    reader.readAsDataURL(blob);

    reader.onloadend = function () {
        const base64DataImage = reader.result;
        formData.value.image = base64DataImage;
    }
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
            <form @submit.prevent="register" class="left-block-content" enctype="multipart/form-data">
                <WFInput label="Firstname" name="firstName" placeholder="Input your firstname"
                    v-model:value="formData.firstName" autocomplete="given-name" />
                <WFInput label="Lastname" name="lastName" placeholder="Input your lastname"
                    v-model:value="formData.lastName" autocomplete="family-name" />
                <WFInput label="Email" type="email" name="email" placeholder="Input your email"
                    v-model:value="formData.email" autocomplete="email" />
                <WFDropdown :options="cities" placeholder="Select a City" id="cityDropdown" error="This field is required"
                    :isError="errors[0] === true" editable v-model:value="formData.selectedCity" :labelOnTop="true" />
                <WFInput label="Password" type="password" name="password" placeholder="Input your password"
                    v-model:value="formData.password" minLength="6" error="Input the same passwords"
                    :isError="errors[1] === true" autocomplete="new-password" />
                <WFInput label="Password" type="password" name="samePassword" placeholder="Input the same password"
                    v-model:value="formData.samePassword" minLength="6" error="Input the same passwords"
                    :isError="errors[1] === true" autocomplete="new-password" />
                <FileUpload mode="basic" accept="image/*" :maxFileSize="1000000" @select="onImageSelect"
                    class="fileUpload" />
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