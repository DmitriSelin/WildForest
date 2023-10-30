<script setup>
import { ref, onMounted } from 'vue';
import { useToast } from "primevue/usetoast";
import { useWeatherStore } from "@/stores/WeatherStore";

const weatherForecasts = ref({});
const toast = useToast();
const weatherStore = useWeatherStore();

onMounted(async () => {
    const result = await weatherStore.getHomeWeather();

    if (result.isError === false) {
        weatherForecasts.value = result.data;
        weatherForecasts.value.sort((a, b) => new Date(a.date) - new Date(b.date));
    }
    else {
        toast.add({ severity: 'error', summary: 'Error', detail: result.data, life: 10000 });
    }
});
</script>

<template>
    <main class="main">
        <div class="main-weather-temperature">
            <h1>CityName</h1>
            <h2>Date</h2>
            <img src="../assets/images/logo.ico" alt="weatherIcon">
            <h2>Sunny</h2>
        </div>
        <div class="main-weather-data">
            <div class="main-weather-data-wind">

            </div>
            <div class="main-weather-data-one">

            </div>
            <div class="main-weather-data-one">

            </div>
        </div>
        <div class="main-weather-data">
            <div class="main-weather-data-wind">

            </div>
            <div class="main-weather-data-one">

            </div>
            <div class="main-weather-data-one">

            </div>
        </div>
        <Toast />
    </main>
</template>

<style lang="scss" scoped></style>