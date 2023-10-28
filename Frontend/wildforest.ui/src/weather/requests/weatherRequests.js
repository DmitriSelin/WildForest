import ky from "ky";
import { url } from "@/infrastructure/urls/urlUtility";
import { refreshToken } from "@/api/api";
import { useUserStore } from "@/stores/UserStore"

const UserStore = useUserStore();

export async function getHomeWeatherForecast(token) {
    const currentDate = new Date().toJSON().slice(0, 10);

    try {
        const result = await ky.get(`${url}homeCity/${currentDate}`);
    }
    catch (err) {
        if (err.response.status === 401) {
            try {
                const result = await refreshToken();
            }
            catch (err) {
                return { data: err.response, isError: true }
            }
        }
    }
}