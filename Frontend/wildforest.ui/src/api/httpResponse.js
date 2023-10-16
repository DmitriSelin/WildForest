export class HttpResponse {
    constructor (data, isError) {
        this.data = data;
        this.isError = isError;
    }
}