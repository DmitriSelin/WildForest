import { defineStore } from "pinia";
import { ref } from 'vue';
import { url } from "@/infrastructure/urls/urlUtility"
import ky from 'ky';

export const useUserStore = defineStore("userStore", () => {
    const authCredentials = ref({ languages: [], countries: [] });
    const registerResponse = ref({});

    const getAuthCredentials = async () => {
        try {
            authCredentials.value = await ky.get(`${url}auth/countries-languages`).json();
        }
        catch (error) {
            console.log("Server error");
        }
    };

    const register = async (request) => {
        registerResponse.value = await ky.post(`${url}auth/register`, { json: request }).json();
    }

    return {
        registerResponse,
        authCredentials,
        getAuthCredentials,
        register
    };
});