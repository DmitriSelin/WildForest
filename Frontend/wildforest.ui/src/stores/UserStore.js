import { defineStore } from "pinia";
import { ref } from 'vue';
import ky from 'ky';

const url = window.location.host;

export default defineStore("userStore", () => {
    const registerResponse = ref({});

    const register = async (registerRequest, to) => {
        registerResponse.value = await ky.post(`${url}${to}`, {json: registerRequest}).json();
    }
});