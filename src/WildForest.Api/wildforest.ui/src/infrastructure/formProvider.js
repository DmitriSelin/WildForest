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
    const formData = {
        firstName: userStore.authResponse.firstName,
        lastName: userStore.authResponse.lastName,
        email: userStore.authResponse.email,
        selectedCity: { id: userStore.authResponse.cityId, name: userStore.authResponse.cityName },
        selectedLanguage: { id: userStore.authResponse.languageId, name: userStore.authResponse.language },
        password: "",
        newPassword: null
    };

    return formData;
}