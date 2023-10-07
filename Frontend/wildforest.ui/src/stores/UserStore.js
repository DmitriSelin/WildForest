import { defineStore } from "pinia";
import { ref } from 'vue';
import { url } from "@/infrastructure/urls/urlUtility"
import ky from 'ky';

export const useUserStore = defineStore("userStore", () => {
    const registerResponse = ref({});
    const registerCredentials = ref({});

    const getRegisterCredentials = async () => {
        registerCredentials.value = await ky.get(`${url}auth/countries-languages`).json();
    }

    const register = async (request) => {
        const registerRequest = JSON.stringify(request);
        registerResponse.value = await ky.post(`${url}auth/register`, {json: registerRequest}).json();
    }

    return {
        registerCredentials,
        registerResponse,
        register
    };
});