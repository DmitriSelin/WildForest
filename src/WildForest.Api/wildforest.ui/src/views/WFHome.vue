<script setup>
import { ref, onMounted } from 'vue';
import WFWeatherTabs from '../components/tabs/WFWeatherTabs.vue'
import { useToast } from "primevue/usetoast";
import { useWeatherStore } from "@/stores/WeatherStore";
import { isFirstLoaded } from '@/infrastructure/storageUtils';
import { getTodayWeatherForecast, getClosestTime } from '@/infrastructure/dateTimeProvider';

const weatherForecasts = ref({});
const toast = useToast();
const weatherStore = useWeatherStore();
const currentForecast = ref({});

onMounted(async () => {
    if (isFirstLoaded("homePage") === false)
        return;

    const result = await weatherStore.getHomeWeather();

    if (result.isError === false) {
        weatherForecasts.value = result.data;
        weatherForecasts.value.sort((a, b) => new Date(a.date) - new Date(b.date));
        const todayForecasts = getTodayWeatherForecast(weatherForecasts.value);
        currentForecast.value = getClosestTime(todayForecasts);
    }
    else {
        toast.add({ severity: 'error', summary: 'Error', detail: result.data, life: 10_000 });
    }
});
</script>

<template>
    <main class="main">
        <WFWeatherTabs :tabs="weatherForecasts" />
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