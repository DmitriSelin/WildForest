<script setup>
import WFInput from '@/components/inputs/WFInput.vue';
import WFButton from '@/components/buttons/WFButton.vue';
import WFDropdown from "@/components/inputs/WFDropdown.vue";
import { getProfileFormData } from "@/infrastructure/formProvider";
import { UserService } from "@/users/userService";
import { SUCCESS, ERROR } from "@/api/apiConstants";
import { ERROR_SEVERITY, SUCCESS_SEVERITY, STANDARD_LIFE } from "@/infrastructure/components/toasts/toastConstants";
import { useToast } from "primevue/usetoast";
import { ref, computed, onMounted } from 'vue';

const buttonType = ref("button");
const formData = ref(getProfileFormData());
const buttonLabel = computed(() => {
    return buttonType.value === "button" ? "Edit" : "Save";
});
const formDisabled = computed(() => {
    return buttonType.value === 'button' ? true : false;
});
const profileCredentials = ref({ cities: [], languages: [] });
const userService = new UserService();
const toast = useToast();

onMounted(async () => {
    const requestResult = await userService.getLanguagesAndCities();

    if (requestResult.result === SUCCESS) {
        profileCredentials.value = requestResult.data;
    }
    else if (requestResult.result === ERROR) {
        toast.add({ severity: ERROR_SEVERITY, summary: 'Error', detail: requestResult.data.title, life: STANDARD_LIFE });
    }
});

const saveChanges = async () => {
    const request = getRequestData(formData.value);
    const requestResult = await userService.updateProfile(request);

    if (requestResult.result === SUCCESS) {
        buttonType.value = "button";
        formData.value.password = "";
        formData.value.newPassword = "";
        toast.add({ severity: SUCCESS_SEVERITY, summary: 'Success', detail: 'The profile has been updated', life: STANDARD_LIFE });
    }
    else if (requestResult.result === ERROR) {
        toast.add({ severity: ERROR_SEVERITY, summary: 'Error', detail: requestResult.data.title, life: STANDARD_LIFE });
    }
}

const edit = () => {
    if (buttonType.value === "button")
        buttonType.value = "submit";
}

const clearForm = () => {
    buttonType.value = "button";
    formData.value.password = "";
    formData.value.newPassword = "";
}

function getRequestData(formData) {
    const request = {
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        password: formData.password,
        newPassword: formData.newPassword,
        cityId: formData.selectedCity.id,
        languageId: formData.selectedLanguage.id
    };

    return request;
}
</script>

<template>
    <main class="main">
        <form class="form" @submit.prevent="saveChanges">
            <WFInput label="Firstname" name="firstName" placeholder="Input your firstname" :disabled="formDisabled"
                v-model:value="formData.firstName" />
            <WFInput label="Lastname" name="lastName" placeholder="Input your lastname" :disabled="formDisabled"
                v-model:value="formData.lastName" />
            <WFInput label="Email" type="email" name="email" placeholder="Input your email" :disabled="true"
                v-model:value="formData.email" />
            <WFDropdown :options="profileCredentials.cities" placeholder="Select a city" id="cityDropdown"
                v-model:value="formData.selectedCity" :disabled="formDisabled" :editable="true" />
            <WFDropdown :options="profileCredentials.languages" placeholder="Select a language" id="languageDropdown"
                v-model:value="formData.selectedLanguage" :disabled="formDisabled" />
            <WFInput label="Old password" type="password" name="oldPassword" placeholder="Input your old password"
                :disabled="formDisabled" minLength="6" v-model:value="formData.password" />
            <WFInput label="New password (optional)" type="password" name="newPassword"
                placeholder="Input your new password" :disabled="formDisabled" minLength="6" :required="false"
                v-model:value="formData.newPassword" />
            <div class="buttons">
                <WFButton label="Cancel" type="reset" :disabled="formDisabled" @click="clearForm" />
                <WFButton :label="buttonLabel" color="success" :type="buttonType" @click="edit" />
            </div>
        </form>
        <Toast />
    </main>
</template>

<style lang="scss" scoped>
.main {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;

    .form {
        display: flex;
        flex-direction: column;
        gap: 3vh;

        .buttons {
            display: flex;
            justify-content: space-between;
        }
    }
}
</style>