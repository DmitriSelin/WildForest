import { defineStore } from "pinia";
import { ref } from 'vue';
import { url } from "@/infrastructure/urls/urlUtility"
import ky from 'ky';

export const useUserStore = defineStore("userStore", () => {
    const authCredentials = ref({});
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
        const registerRequest = JSON.stringify(request);
        registerResponse.value = await ky.post(`${url}auth/register`, { json: registerRequest }).json();
    }

    return {
        registerResponse,
        authCredentials,
        getAuthCredentials,
        register
    };
});