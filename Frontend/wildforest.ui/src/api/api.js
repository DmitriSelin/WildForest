import { url } from "@/infrastructure/urls/urlUtility";
import { HttpResponse } from "./httpResponse";
import ky from 'ky';

export const get = async (path) => {
    try {
        const result = await ky.get(`${url}${path}`).json();
        return new HttpResponse(result, false);
    }
    catch (err) {
        return new HttpResponse(err, true);
    }
}

export const post = async (path, request) => {
    try {
        const result = await ky.post(`${url}${path}`, { json: request }).json();
        return new HttpResponse(result, false);
    }
    catch (err) {
        return new HttpResponse(err, true);
    }
}