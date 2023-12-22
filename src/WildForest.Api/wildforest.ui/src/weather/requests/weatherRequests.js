import { useUserStore } from "@/stores/UserStore"

export async function getHomeWeatherForecast() {
    const userStore = useUserStore();
    const currentDate = new Date().toJSON().slice(0, 10);
}