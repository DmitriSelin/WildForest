import { defineStore } from "pinia";
import { ref, watch } from 'vue';
import { getHomeWeatherForecast } from '@/weather/requests/weatherRequests';

export const useWeatherStore = defineStore("weatherStore", () => {
    //State
    const weatherForecasts = ref({ data: [], errorMessage: null });

    //Actions
    const getHomeWeather = async () => {
        const result = sessionStorage.getItem("weatherForecasts");

        if (result === null) {
            getWeatherFromApi();
        }
        else {
            weatherForecasts.value = result;
            weatherForecasts.value.errorMessage = null;
        }
    }

    async function getWeatherFromApi() {
        const result = await getHomeWeatherForecast();

        if (result.isError === false) {
            weatherForecasts.value.data = result.data;
            weatherForecasts.value.errorMessage = null;
            sessionStorage.setItem("weatherForecasts", result.data);
        }
        else {
            weatherForecasts.value.errorMessage = result.data;
        }
    }

    return {
        weatherForecasts,
        getHomeWeather
    };
});