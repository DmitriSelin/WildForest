import { url } from "@/infrastructure/urls/urlUtility";
import { GET, POST, PUT, DELETE, SUCCESS, ERROR, UNAUTHORIZED } from "./apiConstants";
import { RequestResult } from "./requestResult";
import {getItemFromLocalStorage} from "@/infrastructure/storage/storageUtils";
import router from "@/router/router";
import ky from 'ky';

export function goTo(routeName) {
    router.push({ name: routeName });
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

export async function request(path, method, requestData = null, headers = null) {
    switch (method) {
        case GET:
            return await get(path, requestData, headers);
        case POST:
        case PUT:
        case DELETE:
            return await requestWithBody(path, method, requestData, headers);
        default:
            throw new Error("Invalid http method");
    }
}

export async function requestWithPayload(path, method, requestData = null) {
    const headers = getAuthHeader();
    const response = await request(path, method, requestData, headers);

    if (response.result === SUCCESS)
        return response;
    else if (response.result === ERROR) {
        if (response.data.response.status === UNAUTHORIZED) {
            await request(`${url}tokens/refreshToken`, POST);
            await requestWithPayload(path, method, requestData);
        }
        else {
            return response;
        }
    }
}

function getAuthHeader() {
    const token = getItemFromLocalStorage("authResponse").token;

    const headers = {
        Authorization: `Bearer ${token}`
    };

    return headers;
}

async function get(path, requestData, headers) {
    let requestParameters = '';

    if (requestData !== null && requestData !== undefined)
        requestParameters = `?${Object.keys(requestData).map(key => `${key}=${requestData[key]}`).join('&')}`;

    try {
        const response = await ky.get(`${url}${path}${requestParameters}`, { headers: headers }).json();
        return new RequestResult(SUCCESS, response);
    }
    catch (err) {
        return new RequestResult(ERROR, err);
    }
}

async function requestWithBody(path, method, requestData, headers) {
    try {
        const response = await requestWithKy(path, method, requestData, headers);
        return new RequestResult(SUCCESS, response);
    }
    catch (err) {
        return new RequestResult(ERROR, err);
    }
}

async function requestWithKy(path, method, requestData, headers) {
    switch (method) {
        case POST:
            return await ky.post(`${url}${path}`, { headers: headers, json: requestData }).json();
        case PUT:
            return await ky.put(`${url}${path}`, { headers: headers, json: requestData }).json();
        case DELETE:
            return await ky.post(`${url}${path}`, { headers: headers, json: requestData }).json();
    }
}