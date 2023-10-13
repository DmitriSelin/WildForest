import { defineStore } from "pinia";
import { ref } from 'vue';
import { url } from "@/infrastructure/urls/urlUtility"
import ky from 'ky';

export const useUserStore = defineStore("userStore", () => {
    const authCredentials = ref({ languages: [], countries: [] });
    const registerResponse = ref({});
    const selectedCredentials = ref({selectedCountry: {}, selectedLanguage: {}});

    const getAuthCredentials = async () => {
        try {
            authCredentials.value = await ky.get(`${url}auth/countries-languages`).json();
        }
        catch (error) {
            console.log("Server error");
        }
    };

    const setAuthCredentials = (country, language) => {
        selectedCredentials.value.selectedCountry = country;
        selectedCredentials.value.selectedLanguage = language;
    };

    const register = async (request) => {
        registerResponse.value = await ky.post(`${url}auth/register`, { json: request }).json();
    }

    return {
        registerResponse,
        authCredentials,
        setAuthCredentials,
        getAuthCredentials,
        register
    };
});