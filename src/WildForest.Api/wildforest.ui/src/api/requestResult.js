import { SUCCESS, ERROR } from "./apiConstants";

export class RequestResult {
    constructor(result, data) {
        if (result !== SUCCESS && result !== ERROR)
            throw new Error("Invalid result of request");

        this.result = result;
        this.data = data;
    }
}