import ky from "ky";
import { url } from "@/infrastructure/urls/urlUtility";
import { useUserStore } from "@/stores/UserStore"

export async function getHomeWeatherForecast() {
    const userStore = useUserStore();
    const currentDate = new Date().toJSON().slice(0, 10);

    try {
        const token = userStore.authResponse.token;
        const headers = {
            Authorization: `Bearer ${token}`
        };
        const result = await ky.get(`${url}weather/forecasts/homeCity/${currentDate}`, { headers });

        return { data: result, isError: false }
    }
    catch (err) {
        const status = err.response.status;
        const title = await err.response.json().title;
        if (status === 401) {
            try {
                const result = await userStore.refresh();

                if (result === true) {
                    return await getHomeWeatherForecast();
                }
                else {
                    return { data: "Internal server error", isError: true };
                }
            }
            catch (err) {
                return { data: "Internal server error", isError: true };
            }
        }
        else if (status === 400) {
            return { data: "Invalid auth credentials", isError: true };
        }
        else if (status === 404) {
            return { data: title, isError: true };
        }
    }
}