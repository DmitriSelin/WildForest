import * as signalR from "@microsoft/signalr";
import { GET_COMMENTS, GET_COMMENTS_ASYNC } from "@/api/apiConstants";
import { WeatherService } from "@/weather/weatherService";
import { getItemFromSessionStorage } from "@/infrastructure/storage/storageUtils";
import { WEATHER_STORAGE_NAME } from "@/weather/weatherConstants";

export class CommentService {
    constructor(url, onError) {
        this.connection = new signalR
            .HubConnectionBuilder()
            .withUrl(url)
            .build();

        this.#start(onError);
    }

    sendComment(methodName, data, onError) {
        this.connection.invoke(methodName, data)
            .catch(function (err) {
                onError();
            });
    }

    getComments() {
        
    }

    #start(onError) {
        this.connection.start()
            .then(() => {
                const weatherForecastId = this.#getWeatherForecastId();
                let result = this.connection.invoke(GET_COMMENTS, weatherForecastId);
            })
            .catch(function (err) {
                onError();
            });
    }

    #getWeatherForecastId() {
        const weatherService = new WeatherService();
        const weatherForecasts = getItemFromSessionStorage(WEATHER_STORAGE_NAME);
        const todayForecast = weatherService.getTodayWeatherForecast(weatherForecasts.data);
        return todayForecast.weatherForecastId;
    }
}