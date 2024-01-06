<script setup>
import WFRating from '@/components/radiobuttons/WFRating.vue';
import { ref, onMounted } from 'vue';
import WFWeatherTabs from '@/components/tabs/WFWeatherTabs.vue'
import { WeatherService } from '@/weather/weatherService';
import { getCurrentDateInfo } from '@/infrastructure/dateTimeProvider';
import { SUCCESS } from "@/api/apiConstants";
import { ERROR_SEVERITY, STANDARD_LIFE } from "@/infrastructure/components/toasts/toastConstants";
import { useToast } from "primevue/usetoast";
import { useUserStore } from "@/stores/UserStore";

const weatherService = new WeatherService();
const todayForecast = ref({});
const currentForecast = ref({});
const userStore = useUserStore();
const toast = useToast();

onMounted(async () => {
    const requestResult = await weatherService.getHomeWeatherForecasts();

    if (requestResult.result === SUCCESS) {
        todayForecast.value = requestResult.data;
        currentForecast.value = weatherService.getCurrentForecast(todayForecast.value);
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
                        <font-awesome-icon icon="fa-solid fa-cloud-rain" class="weather-content-info-data-img" />
                        <h1>{{ currentForecast.temperature.value }}&nbsp;°C</h1>
                    </div>
                    <h2>Rain</h2>
                </div>
                <WFRating rating="7.8" />
            </div>
            <WFWeatherTabs :tabs="todayForecast.weatherForecasts" :selectedTab="currentForecast.time"
                style="margin-top: 2vh;" />
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

        &-info {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;

            &-data {
                display: flex;
                gap: 30px;
                margin: 3vh 0;

                &-img {
                    height: 100px;
                }
            }
        }
    }
}

h2,
h3 {
    margin: 0;
}
</style>