import { useUserStore } from "@/stores/UserStore";
import { getItemFromLocalStorage } from "@/infrastructure/storage/storageUtils";

export function getLoginFormData() {
    const formData = {
        email: "",
        password: ""
    }

    return formData;
}

export function getRegisterFormData() {
    const formData = {
        firstName: "",
        lastName: "",
        email: "",
        selectedCity: {},
        password: "",
        samePassword: ""
    }

    return formData;
}

export function getAuthCredentialsFormData() {
    const formData = {
        selectedLanguage: {},
        selectedCountry: {}
    }

    return formData;
}

export function getProfileFormData() {
    const userStore = useUserStore();
    const selectedCredentials = getItemFromLocalStorage("selectedCredentials")._value;
    const languageName = selectedCredentials.selectedLanguage.name;
    const formData = {...userStore.authResponse, languageName: languageName, newPassword: ""};
    formData.password = "";
    return formData;
}