import { defineStore } from "pinia";
import { ref } from 'vue';
import ky from 'ky';

const url = window.location.host + "/api/";

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