import { defineStore } from "pinia";
import { ref, watch } from 'vue';
import { Api } from "@/api/api";
import { SUCCESS, ERROR, GET, POST, PUT } from "@/api/apiConstants";

export const useUserStore = defineStore("userStore", () => {
    const api = new Api();
    // State
    const authCredentials = ref({ languages: [], countries: [] });
    const authResponse = ref({});
    const selectedCredentials = ref({ selectedCountry: {}, selectedLanguage: {} });
    const cities = ref([]);
    const errorMessage = ref("");
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

    const getAuthCredentials = async () => {
        const requestResult = await api.request("auth/countries-languages", GET);

        if (requestResult.result === SUCCESS) {
            authCredentials.value = requestResult.data;
        }
    };

    const setAuthCredentials = (country, language) => {
        selectedCredentials.value.selectedCountry = country;
        selectedCredentials.value.selectedLanguage = language;
    };

    const getCitiesByCountry = async () => {
        const requestResult = await api.request(`auth/cities/${selectedCredentials.value.selectedCountry.id}`, GET);

        if (requestResult.result === SUCCESS) {
            cities.value = requestResult.data;
        }
    }

    const register = async (request) => {
        request.languageId = selectedCredentials.value.selectedLanguage.id;
        const requestResult = await api.request('auth/register', POST, request);

        if (requestResult.result === SUCCESS) {
            authResponse.value = requestResult.data;
            return true;
        }
        else if (requestResult.result === ERROR) {
            const message = await requestResult.data.json();
            errorMessage.value = message.title;
            return false;
        }
    }

    const login = async (request) => {
        const requestResult = await api.request(`auth/login`, POST, request);

        if (requestResult.result === SUCCESS) {
            authResponse.value = requestResult.data;
            return true;
        }
        else if (requestResult.result === ERROR) {
            const message = await requestResult.data.json();
            errorMessage.value = message.title;
            return false;
        }
    }

    const updateAuthResponse = (newAuthResponse) => {
        authResponse.value = newAuthResponse;
    }
    // Actions

    return {
        authResponse,
        authCredentials,
        cities,
        errorMessage,
        setAuthCredentials,
        getCitiesByCountry,
        getAuthCredentials,
        register,
        login,
        updateAuthResponse
    };
});