import { defineStore } from "pinia";
import { ref, watch } from 'vue';
import { get, post } from "../api/api";

export const useUserStore = defineStore("userStore", () => {
    const authCredentials = ref({ languages: [], countries: [] });
    const registerResponse = ref({});
    const selectedCredentials = ref({ selectedCountry: {}, selectedLanguage: {} });
    const cities = ref([]);

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

        if (!response.isError) {
            cities.value = response.data;
        }
    }

    const register = async (request) => {
        const response = await post("auth/register", request);

        if (!response.isError) {
            registerResponse.value = response.data;
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
        setAuthCredentials,
        getCitiesByCountry,
        getAuthCredentials,
        register
    };
});