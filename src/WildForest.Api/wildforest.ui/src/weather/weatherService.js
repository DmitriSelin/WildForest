import { Api } from "@/api/api";
import { WEATHER_STORAGE_NAME } from "./weatherConstants";
import { getItemFromSessionStorage, setItemInSessionStorage } from "@/infrastructure/storage/storageUtils";
import { SUCCESS, ERROR, GET } from "@/api/apiConstants";
import { getTodayDate, getClosestTime } from "@/infrastructure/dateTimeProvider";
import { RequestResult } from "@/api/requestResult";

export class WeatherService {
    constructor(api = new Api()) {
        this.api = api;
    }

    async getHomeWeatherForecasts() {
        try {
            const weatherResult = getItemFromSessionStorage(WEATHER_STORAGE_NAME);
            return this.#createWeatherForecast(weatherResult);
        }
        catch (err) {
            const todayDate = getTodayDate();
            const weatherResult = await this.api.requestWithPayload(`weather/forecasts/homeCity/${todayDate}`, GET);

            if (weatherResult.result === SUCCESS) {
                setItemInSessionStorage(WEATHER_STORAGE_NAME, weatherResult);
                return this.#createWeatherForecast(weatherResult);
            }
            else if (weatherResult.result === ERROR) {
                const badResponse = await weatherResult.data.json();
                return new RequestResult(ERROR, badResponse);
            }
        }
    }

    getCurrentForecast(todayForecast) {
        const currentTime = getClosestTime(todayForecast.weatherForecasts);
        const currentForecast = todayForecast.weatherForecasts.find(x => x.time === currentTime);
        return currentForecast;
    }

    #createWeatherForecast(weatherResult) {
        const todayForecast = this.#getTodayWeatherForecast(weatherResult.data);
        this.#formatTimeToHoursAndMinutes(todayForecast.weatherForecasts);
        todayForecast.weatherForecasts.sort((a, b) => a.time.localeCompare(b.time));
        return new RequestResult(SUCCESS, todayForecast);
    }

    #getTodayWeatherForecast(forecasts) {
        const currentDate = getTodayDate();
        const todayForecast = forecasts.find(x => x.date === currentDate);
        return todayForecast;
    }

    #formatTimeToHoursAndMinutes(forecasts) {
        for (let i = 0; i < forecasts.length; i++) {
            forecasts[i].time = forecasts[i].time.split(':').slice(0, 2).join(':');
        }
    }
}