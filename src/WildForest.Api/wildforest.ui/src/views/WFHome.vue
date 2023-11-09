<script setup>
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
        <WFWeatherTabs :tabs="currentForecast.weatherForecasts" :selectedTab="currentTime" />
        <div class="main-weather-temperature">
            <h1>CityName</h1>
            <h2>Date</h2>
            <img src="../assets/images/logo.ico" alt="weather icon">
            <h2>Sunny</h2>
        </div>
        <Toast />
    </main>
</template>

<style lang="scss" scoped></style>