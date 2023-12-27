import { Api } from "@/api/api";

export class WeatherRequests {
    constructor (api = new Api()) {
        this.api = api;
    }

    async getHomeWeatherForecasts() {
        
    }
}