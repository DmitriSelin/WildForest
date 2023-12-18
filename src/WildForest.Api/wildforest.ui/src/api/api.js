import { url } from "@/infrastructure/urls/urlUtility";
import router from "@/router/router";
import ky from 'ky';

export const get = async (path) => {
    try {
        const result = await ky.get(`${url}${path}`).json();
        return { data: result, isError: false };
    }
    catch (err) {
        return { data: err, isError: true };
    }
}

export const post = async (path, request) => {
    try {
        const result = await ky.post(`${url}${path}`, { json: request }).json();
        return { data: result, isError: false };
    }
    catch (err) {
        return { data: err, isError: true };
    }
}

export const refreshToken = async () => {
    try {
        const result = await ky.post(`${url}tokens/refreshToken`).json();
        return { data: result, isError: false };
    }
    catch (err) {
        return { data: err.response, isError: true };
    }
}

export const getAuthHeader = (token) => {
    const headers = {
        Authorization: `Bearer ${token}`
    };

    return headers;
}

export function goTo(routeName) {
    router.push({ name: routeName });
}