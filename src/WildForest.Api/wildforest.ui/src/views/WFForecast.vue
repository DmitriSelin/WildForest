<script setup>
import WFWeatherTabs from '@/components/tabs/WFWeatherTabs.vue';
import { WeatherService } from "../weather/weatherService";
import { ref, onMounted } from 'vue';
import { SUCCESS, ERROR } from "@/api/apiConstants";
import { ERROR_SEVERITY, STANDARD_LIFE } from "@/infrastructure/components/toasts/toastConstants";
import { useToast } from "primevue/usetoast";
import { getTodayDate } from "@/infrastructure/dateTimeProvider";

const weatherService = new WeatherService();
const weatherForecasts = ref([]);
const currentDate = ref(getTodayDate());
const toast = useToast();

onMounted(() => {
    const requestResult = weatherService.getFiveDaysWeatherForecasts();

    if (requestResult.result === SUCCESS) {
        weatherForecasts.value = requestResult.data;
    }
    else if (requestResult.result === ERROR) {
        toast.add({ severity: ERROR_SEVERITY, summary: 'Error', detail: requestResult.data.title, life: STANDARD_LIFE });
    }
});
</script>

<template>
    <main class="main">
        <WFWeatherTabs :tabs="weatherForecasts" :selectedTab="currentDate"/>
        <Toast/>
    </main>
</template>

<style lang="scss" scoped>
.main {
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>