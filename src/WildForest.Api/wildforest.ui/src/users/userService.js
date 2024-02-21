import { Api } from "@/api/api";
import { useUserStore } from "@/stores/userStore";
import { SUCCESS, ERROR, GET, PUT, POST } from "@/api/apiConstants";
import { RequestResult } from "@/api/requestResult";
import { getItemFromSessionStorage, setItemInSessionStorage } from "@/infrastructure/storage/storageUtils";
import { PROFILE_CREDENTIALS_STORAGE_NAME, UP, DOWN } from "./userConstants";

export class UserService {
    constructor(api = new Api()) {
        this.api = api;
        this.userStore = useUserStore();
    }

    async updateProfile(updateProfileRequest) {
        const requestResult = await this.api.requestWithPayload("users/profile", PUT, updateProfileRequest);

        if (requestResult.result === SUCCESS) {
            this.userStore.setAuthResponse(requestResult.data);
            return requestResult;
        }
        else if (requestResult.result === ERROR) {
            return await this.#returnBadRequest(requestResult);
        }
    }

    async getLanguagesAndCities() {
        try {
            const requestResult = getItemFromSessionStorage(PROFILE_CREDENTIALS_STORAGE_NAME);
            return requestResult;
        }
        catch (err) {
            const requestResult = await this.api.requestWithPayload("users/languages-cities", GET);

            if (requestResult.result === SUCCESS) {
                setItemInSessionStorage(PROFILE_CREDENTIALS_STORAGE_NAME, requestResult);
                return requestResult;
            }
            else if (requestResult.result === ERROR) {
                return await this.#returnBadRequest(requestResult);
            }
        }
    }

    async vote(voteResult, ratingId, voteId) {
        const request = { ratingId: ratingId, userId: this.userStore.authResponse.id }
        let requestResult;

        if (voteId) {
            const updationRequest = { ...request, id: voteId }
            requestResult = await this.api.requestWithPayload("weather/rating/vote", PUT, updationRequest);

            return await this.#returnRequest(requestResult);
        }

        if (voteResult === UP) {
            requestResult = await this.api.requestWithPayload("weather/rating/vote/up", POST, request);
        }
        else if (voteResult === DOWN) {
            requestResult = await this.api.requestWithPayload("weather/rating/vote/down", POST, request);
        }

        return await this.#returnRequest(requestResult);
    }

    async getVote(ratingId) {
        const request = { ratingId: ratingId, userId: this.userStore.authResponse.id }

        const requestResult = await this.api.requestWithPayload("weather/rating/vote", POST, request);
        return await this.#returnRequest(requestResult);
    }

    async getAuthCredentials() {
        const requestResult = await this.api.request("auth/countries-languages", GET);
        return await this.#returnRequest(requestResult);
    }

    async getCitiesByCountry() {
        const selectedCountryId = this.userStore.selectedCredentials.selectedCountry.id;
        const requestResult = await this.api.request(`auth/cities/${selectedCountryId}`, GET);
        return await this.#returnRequest(requestResult);
    }

    async register(request) {
        request.languageId = this.userStore.selectedCredentials.selectedLanguage.id;
        const requestResult = await this.api.request('auth/register', POST, request);

        if (requestResult.result === SUCCESS) {
            this.userStore.setAuthResponse(requestResult.data);
            return requestResult;
        }
        else if (requestResult.result === ERROR) {
            return await this.#returnBadRequest(requestResult);
        }
    }

    async login(request) {
        const requestResult = await this.api.request(`auth/login`, POST, request);

        if (requestResult.result === SUCCESS) {
            this.userStore.setAuthResponse(requestResult.data);
            return requestResult;
        }
        else if (requestResult.result === ERROR) {
            return await this.#returnBadRequest(requestResult);
        }
    }

    async #returnRequest(requestResult) {
        if (requestResult.result === SUCCESS) {
            return requestResult;
        }
        else if (requestResult.result === ERROR) {
            return await this.#returnBadRequest(requestResult);
        }
    }

    async #returnBadRequest(requestResult) {
        const badResponse = await requestResult.data.json();
        return new RequestResult(ERROR, badResponse);
    }
}