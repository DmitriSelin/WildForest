import { defineStore } from "pinia";
import { ref, watch } from 'vue';

export const useUserStore = defineStore("userStore", () => {
    // State
    const authResponse = ref({});
    const selectedCredentials = ref({ selectedCountry: {}, selectedLanguage: {} });
    // State

    // Actions
    const selectedCredentialsInLocalStorage = localStorage.getItem("selectedCredentials");
    if (selectedCredentialsInLocalStorage) {
        selectedCredentials.value = JSON.parse(selectedCredentialsInLocalStorage)._value;
    }

    watch(() => selectedCredentials, (state) => {
        localStorage.setItem("selectedCredentials", JSON.stringify(state));
    },
        { deep: true }
    );

    const authResponseInLocalStorage = localStorage.getItem("authResponse");
    if (authResponseInLocalStorage) {
        authResponse.value = JSON.parse(authResponseInLocalStorage)._value;
    }

    watch(() => authResponse, (state) => {
        localStorage.setItem("authResponse", JSON.stringify(state));
    },
        { deep: true }
    );

    const setAuthCredentials = (country, language) => {
        selectedCredentials.value.selectedCountry = country;
        selectedCredentials.value.selectedLanguage = language;
    };

    const setAuthResponse = (newAuthResponse) => {
        authResponse.value = newAuthResponse;
    }
    // Actions

    return {
        authResponse,
        selectedCredentials,
        setAuthCredentials,
        setAuthResponse
    };
});