import { url } from "@/infrastructure/urls/urlUtility";
import { HttpResponse } from "./httpResponse";
import ky from 'ky';

export async function register(request) {
    try {
        const result = await ky.post(`${url}auth/register`, { json: request }).json();
        return new HttpResponse(result, false);
    }
    catch (err) {
        return new HttpResponse(err, true);
    }
}