import { Api } from "@/api/api";
import { WEATHER_STORAGE_NAME } from "./weatherConstants";
import { getItemFromSessionStorage, setItemInSessionStorage } from "@/infrastructure/storage/storageUtils";
import { SUCCESS, ERROR, GET } from "@/api/apiConstants";
import { getTodayDate } from "@/infrastructure/dateTimeProvider";
import { RequestResult } from "@/api/requestResult";

export class WeatherService {
    constructor(api = new Api()) {
        this.api = api;
    }

    async getHomeWeatherForecasts() {
        try {
            const weatherResult = getItemFromSessionStorage(WEATHER_STORAGE_NAME);
            const currentForecast = this.#getTodayWeatherForecast(weatherResult.data);
            this.#formatTimeToHoursAndMinutes(currentForecast.weatherForecasts);
            currentForecast.weatherForecasts.sort((a, b) => a.time.localeCompare(b.time));
            return currentForecast;
        }
        catch (err) {
            const api = new Api();
            const todayDate = getTodayDate();
            const weatherResult = await api.requestWithPayload(`weather/forecasts/homeCity/${todayDate}`, GET);

            if (weatherResult.result === SUCCESS)
            {
                setItemInSessionStorage(WEATHER_STORAGE_NAME, weatherResult);
                const currentForecast = this.#getTodayWeatherForecast(weatherResult.data);
                this.#formatTimeToHoursAndMinutes(currentForecast.weatherForecasts);
                currentForecast.weatherForecasts.sort((a, b) => a.time.localeCompare(b.time));
                return currentForecast;
            }
            else if (weatherResult.result === ERROR)
            {
                const badResponse = await weatherResult.data.json();
                return new RequestResult(ERROR, badResponse);
            }
        }
    }

    #getTodayWeatherForecast(forecasts) {
        const currentDate = getTodayDate();
        const currentForecast = forecasts.find(x => x.date === currentDate);
        return currentForecast;
    }

    #formatTimeToHoursAndMinutes(forecasts) {
        for (let i = 0; i < forecasts.length; i++) {
            forecasts[i].time = forecasts[i].time.split(':').slice(0, 2).join(':');
        }
    }
}