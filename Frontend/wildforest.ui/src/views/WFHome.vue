<script setup>
import { ref, onMounted } from 'vue';
import { getHomeWeatherForecast } from '../weather/requests/weatherRequests';

const weatherForecast = ref({});

onMounted(async () => {
    weatherForecast.value = await getHomeWeatherForecast();
    console.log(weatherForecast.value);
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
    </main>
</template>

<style lang="scss" scoped></style>