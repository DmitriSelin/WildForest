<script setup>
import WFRating from '@/components/radiobuttons/WFRating.vue';
import WFWeatherCard from '@/components/weather/WFWeatherCard.vue';
import WFWeatherTabs from '@/components/tabs/WFWeatherTabs.vue'
import { ref, onMounted } from 'vue';
import { WeatherService } from '@/weather/weatherService';
import { getCurrentDateInfo } from '@/infrastructure/dateTimeProvider';
import { SUCCESS } from "@/api/apiConstants";
import { ERROR_SEVERITY, STANDARD_LIFE } from "@/infrastructure/components/toasts/toastConstants";
import { useToast } from "primevue/usetoast";
import { useUserStore } from "@/stores/UserStore";
import getIconFromWeatherName from "@/components/tabs/weatherIconUtils";

const weatherService = new WeatherService();
const todayForecast = ref({ weatherForecasts: [] });
const weatherIcon = ref('');
const userStore = useUserStore();
const toast = useToast();
const currentForecast = ref({
    time: "", pressure: 0, humidity: 0, cloudiness: 0, visibility: 0,
    precipitationProbability: 0, precipitationVolume: 0,
    wind: { speed: 0, direction: 0, gust: 0 }
});

onMounted(async () => {
    const requestResult = await weatherService.getHomeWeatherForecasts();

    if (requestResult.result === SUCCESS) {
        todayForecast.value = requestResult.data;
        currentForecast.value = weatherService.getCurrentForecast(todayForecast.value);
        weatherIcon.value = getIconFromWeatherName(currentForecast.value);
    }
    else {
        toast.add({ severity: ERROR_SEVERITY, summary: 'Error', detail: requestResult.data.title, life: STANDARD_LIFE });
    }
});
</script>

<template>
    <main class="main">
        <div class="weather">
            <div class="weather-content">
                <div class="weather-content-info">
                    <h2 style="margin: 1vh 0 1vh 0;">{{ userStore.authResponse.cityName }}</h2>
                    <h3 style="color: gray;">{{ getCurrentDateInfo() }}</h3>
                    <div class="weather-content-info-data">
                        <font-awesome-icon :icon="`fa-solid fa-${weatherIcon}`" class="weather-content-info-data-img" />
                        <h1>{{ currentForecast.temperature?.value }}&nbsp;°C</h1>
                    </div>
                    <h2>{{ currentForecast.description?.description }}</h2>
                </div>
                <WFRating rating="7.8" />
            </div>
            <WFWeatherTabs :tabs="todayForecast.weatherForecasts" :selectedTab="currentForecast.time"
                style="margin-top: 2vh;" />
            <div class="weather-content-data">
                <div class="container">
                    <WFWeatherCard :value="currentForecast.pressure" title="Pressure" icon="temperature-half"
                        valueType="mmHg" />
                    <WFWeatherCard :value="currentForecast.humidity" title="Humidity" icon="droplet" />
                    <WFWeatherCard :value="currentForecast.cloudiness" title="Cloudiness" icon="cloud" />
                </div>
                <div class="container">
                    <WFWeatherCard :value="currentForecast?.wind.speed" title="Wind speed" icon="wind" valueType="m/s" />
                    <WFWeatherCard :value="currentForecast?.wind.direction" title="Wind direction" icon="compass"
                        valueType="°" />
                    <WFWeatherCard :value="currentForecast?.wind.gust" title="Wind gust" icon="wind" valueType="m/s" />
                </div>
                <div class="container">
                    <WFWeatherCard :value="currentForecast.visibility" title="Visibility" icon="binoculars" valueType="m" />
                    <WFWeatherCard :value="currentForecast.precipitationProbability" title="Precipitation probability"
                        icon="cloud-rain" />
                    <WFWeatherCard :value="currentForecast.precipitationVolume" title="Precipitation volume" icon="percent"
                        valueType="mm" />
                </div>
            </div>
        </div>
        <Toast />
    </main>
</template>

<style lang="scss" scoped>
.weather {
    margin: 3vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    &-content {
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        background-color: var(--violet);
        border-radius: 20px;
        gap: 10vh;
        padding: 40px;
        margin-top: 4vh;

        @media screen and (max-width: 600px) {
            flex-direction: column;
            gap: 2vh;
        }

        &-info {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;

            @media screen and (max-width: 600px) {
                margin-bottom: 15px;
            }

            &-data {
                display: flex;
                gap: 30px;
                margin: 3vh 0;
                align-items: center;

                &-img {
                    height: 70px;

                    @media screen and (max-width: 600px) {
                        height: 40px;
                    }
                }
            }
        }

        &-data {
            display: flex;
            flex-direction: column;

            .container {
                display: flex;
            }
        }
    }
}

h2,
h3 {
    margin: 0;
}
</style>