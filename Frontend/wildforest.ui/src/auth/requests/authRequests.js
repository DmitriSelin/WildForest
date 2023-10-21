import { url } from "@/infrastructure/urls/urlUtility";
import ky from 'ky';

export async function registerUser(request) {
    try {
        const response = await ky.post(`${url}auth/register`, { json: request }).json();
        return { data: response, isError: false };
    }
    catch (err) {
        const status = err.response.status;

        if (status === 409) {
            return { data: "This user is already exists", isError: true };
        }
        else if (status === 404) {
            return { data: "Selected city does not exist", isError: true };
        }

        return { data: "Internal server error", isError: true };
    }
}

export async function loginUser(request) {
    try {
        const response = await ky.post(`${url}auth/login`, { json: request }).json();
        return { data: response, isError: false };
    }
    catch (err) {
        const status = err.response.status;

        if (status === 401) {
            return { data: "User with these credentials does not exist", isError: true };
        }

        return { data: "Internal server error", isError: true };
    }
}