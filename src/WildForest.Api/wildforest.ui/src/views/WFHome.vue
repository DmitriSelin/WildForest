<script setup>
import WFRating from '../components/radiobuttons/WFRating.vue';
import { ref, onMounted } from 'vue';
import WFWeatherTabs from '../components/tabs/WFWeatherTabs.vue'
import { useToast } from "primevue/usetoast";
import { useWeatherStore } from "@/stores/WeatherStore";
import { getTodayWeatherForecast, getClosestTime } from '@/infrastructure/dateTimeProvider';
import { formatTime } from "@/weather/weatherUtils";

const toast = useToast();
const weatherStore = useWeatherStore();
const currentForecast = ref({});
const currentTime = ref("");

onMounted(async () => {
    await weatherStore.getHomeWeather();

    if (weatherStore.weatherForecasts.errorMessage === null) {
        currentForecast.value = getTodayWeatherForecast(weatherStore.weatherForecasts.data);
        currentTime.value = getClosestTime(currentForecast.value.weatherForecasts);
        currentForecast.value.weatherForecasts.sort((a, b) => a.time.localeCompare(b.time));
        formatTime(currentForecast.value.weatherForecasts);
    }
    else {
        toast.add({ severity: 'error', summary: 'Error', detail: 'Message Content', life: 10_000 });
    }
});
</script>

<template>
    <main class="main">
        <div class="weather">
            <div class="weather-content">
                <h2 style="margin: 1vh 0 1vh 0;">New-York</h2>
                <h3 style="color: gray;">10-11-2023, Monday 12:00</h3>
                <div class="weather-content-data">
                    <font-awesome-icon icon="fa-solid fa-cloud-rain" class="weather-content-data-img" />
                    <h1>20 °C</h1>
                </div>
                <h2>Rain</h2>
                <WFRating rating="7.8" views="400"/>
            </div>
            <WFWeatherTabs :tabs="currentForecast.weatherForecasts" :selectedTab="currentTime" style="margin-top: 2vh;"/>
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
        flex-direction: column;
        justify-content: center;
        align-items: center;
        background-color: var(--violet);
        border-radius: 20px;
        padding: 10px 20px;
        margin-top: 20px;

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

h2,
h3 {
    margin: 0;
}
</style>