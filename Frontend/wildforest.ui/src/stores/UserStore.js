import { defineStore } from "pinia";
import { ref } from 'vue';
import { url } from "@/infrastructure/urls/urlUtility"
import ky from 'ky';

export const useUserStore = defineStore("userStore", () => {
    const registerResponse = ref({});

    const register = async (request, to) => {
        registerResponse.value = await ky.post(`${url}${to}`, {json: request}).json();
    }

    return {
        registerResponse,
        register
    };
});