import { defineStore } from "pinia";
import { ref, watch } from 'vue';
import { get } from "../api/api";
import { registerUser, loginUser } from "@/auth/requests/authRequests";

export const useUserStore = defineStore("userStore", () => {
    const authCredentials = ref({ languages: [], countries: [] });
    const registerResponse = ref({});
    const selectedCredentials = ref({ selectedCountry: {}, selectedLanguage: {} });
    const cities = ref([]);
    const errorMessage = ref("");

    const selectedCredentialsInLocalStorage = localStorage.getItem("selectedCredentials");
    if (selectedCredentialsInLocalStorage) {
        selectedCredentials.value = JSON.parse(selectedCredentialsInLocalStorage)._value;
    }

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
            registerResponse.value = response.data;
            return true;
        }
        else {
            errorMessage.value = response.data;
            return false;
        }
    }

    const login = async(request) => {
        const response = await loginUser(request);

        if (response.isError === false) {
            registerResponse.value = response.data;
            return true;
        }
        else {
            errorMessage.value = response.data;
            return false;
        }
    }

    watch(() => selectedCredentials, (state) => {
        localStorage.setItem("selectedCredentials", JSON.stringify(state));
    },
        { deep: true }
    );

    return {
        registerResponse,
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