import { defineStore } from "pinia";
import { ref, watch } from 'vue';
import { get } from "../api/api";
import { registerUser, loginUser } from "@/auth/requests/authRequests";

export const useUserStore = defineStore("userStore", () => {
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
        const response = await get("auth/countries-languages");

        if (!response.isError) {
            authCredentials.value = response.data;
        }
    };

    const setAuthCredentials = (country, language) => {
        selectedCredentials.value.selectedCountry = country;
        selectedCredentials.value.selectedLanguage = language;
    };

    const getCitiesByCountry = async () => {
        const response = await get(`auth/cities/${selectedCredentials.value.selectedCountry.id}`);

        if (response.isError === false) {
            cities.value = response.data;
        }
    }

    const register = async (request) => {
        request.languageId = selectedCredentials.value.selectedLanguage.id;
        const response = await registerUser(request);

        if (response.isError === false) {
            authResponse.value = response.data;
            return true;
        }
        else {
            errorMessage.value = response.data;
            return false;
        }
    }

    const login = async (request) => {
        const response = await loginUser(request);

        if (response.isError === false) {
            authResponse.value = response.data;
            return true;
        }
        else {
            errorMessage.value = response.data;
            return false;
        }
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
        login
    };
});