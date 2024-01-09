import { Api } from "@/api/api";
import { useUserStore } from "@/stores/UserStore";
import { SUCCESS, ERROR, GET, PUT } from "@/api/apiConstants";
import { RequestResult } from "@/api/requestResult";
import { getItemFromSessionStorage, setItemInSessionStorage } from "@/infrastructure/storage/storageUtils";
import { PROFILE_CREDENTIALS_STORAGE_NAME } from "./userConstants";

export class UserService {
    constructor(api = new Api()) {
        this.api = api;
        this.userStore = useUserStore();
    }

    async updateProfile(updateProfileRequest) {
        const requestResult = await this.api.requestWithPayload("users/profile", PUT, updateProfileRequest);

        if (requestResult.result === SUCCESS) {
            this.userStore.updateAuthResponse(requestResult.data);
            return requestResult;
        }
        else if (requestResult.result === ERROR) {
            const badResponse = await requestResult.data.json();
            return new RequestResult(ERROR, badResponse);
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
                const badResponse = await requestResult.data.json();
                return new RequestResult(ERROR, badResponse);
            }
        }
    }
}