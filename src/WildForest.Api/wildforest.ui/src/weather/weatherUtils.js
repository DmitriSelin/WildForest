export function formatTime(forecasts) {
    for (let i = 0; i < forecasts.length; i++) {
        forecasts[i].time = forecasts[i].time.split(':').slice(0, 2).join(':');
    }
}