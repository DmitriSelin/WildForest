import { defineStore } from "pinia";
import { ref, watch } from 'vue';
import { getHomeWeatherForecast } from '@/weather/requests/weatherRequests';

export const useWeatherStore = defineStore("weatherStore", () => {
    //State
    const weatherForecasts = ref({});

    //Actions
    const getHomeWeather = async () => {
        if (weatherForecasts.value !== undefined) {
            return true;
        }
        else {
            const result = await getHomeWeatherForecast();

            return false;
        }
    }

    return {
        weatherForecasts,
        getHomeWeather
    };
});