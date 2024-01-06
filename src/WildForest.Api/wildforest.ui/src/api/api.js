import { url } from "@/infrastructure/urls/urlUtility";
import { GET, POST, PUT, DELETE, SUCCESS, ERROR, UNAUTHORIZED } from "./apiConstants";
import { RequestResult } from "./requestResult";
import { getItemFromLocalStorage } from "@/infrastructure/storage/storageUtils";
import router from "@/router/router";
import ky from 'ky';
import { useUserStore } from "@/stores/UserStore";

export class Api {
    constructor(client = ky) {
        this.client = client;
    }

    async request(path, method, requestData = null, headers = {}) {
        switch (method) {
            case GET:
                return await this.#get(path, requestData, headers);
            case POST:
            case PUT:
            case DELETE:
                return await this.#requestWithBody(path, method, requestData, headers);
            default:
                throw new Error("Invalid http method");
        }
    }

    async requestWithPayload(path, method, requestData = null) {
        let headers = this.#getAuthHeader();
        const response = await this.request(path, method, requestData, headers);

        if (response.result === SUCCESS)
            return response;

        if (response.data.status !== UNAUTHORIZED)
            return response;

        try {
            const authResponse = await this.client.post(`${url}tokens/refreshToken`).json();
            const userStore = useUserStore();
            userStore.updateAuthResponse(authResponse);
            headers = this.#getAuthHeader();

            return await this.request(path, method, requestData, headers);
        }
        catch (err) {
            return new RequestResult(ERROR, err.response);
        }
    }

    #getAuthHeader() {
        const token = getItemFromLocalStorage("authResponse")._value.token;

        const headers = {
            Authorization: `Bearer ${token}`
        };

        return headers;
    }

    async #get(path, requestData, headers) {
        let requestParameters = '';

        if (requestData !== null && requestData !== undefined)
            requestParameters = `?${Object.keys(requestData).map(key => `${key}=${requestData[key]}`).join('&')}`;

        try {
            const response = await this.client.get(`${url}${path}${requestParameters}`, { headers: headers }).json();
            return new RequestResult(SUCCESS, response);
        }
        catch (err) {
            return new RequestResult(ERROR, err.response);
        }
    }

    async #requestWithBody(path, method, requestData, headers) {
        try {
            const response = await this.#requestWithKy(path, method, requestData, headers);
            return new RequestResult(SUCCESS, response);
        }
        catch (err) {
            return new RequestResult(ERROR, err.response);
        }
    }

    async #requestWithKy(path, method, requestData, headers) {
        switch (method) {
            case POST:
                return await this.client.post(`${url}${path}`, { headers: headers, json: requestData }).json();
            case PUT:
                return await this.client.put(`${url}${path}`, { headers: headers, json: requestData }).json();
            case DELETE:
                return await this.client.delete(`${url}${path}`, { headers: headers, json: requestData }).json();
        }
    }
}

export function goTo(routeName) {
    router.push({ name: routeName });
}