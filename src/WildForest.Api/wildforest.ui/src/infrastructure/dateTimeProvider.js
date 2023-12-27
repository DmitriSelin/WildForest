const currentHour = () => {
    return new Date().getHours();
}

export function getClosestTime(forecasts) {
    let closestTime = forecasts[0].time;
    let timeDiff = Math.abs(currentHour() - parseInt(closestTime));

    for (let i = 0; i < forecasts.length; i++) {
        const diff = Math.abs(currentHour() - parseInt(forecasts[i].time));

        if (diff < timeDiff) {
            closestTime = forecasts[i].time;
            timeDiff = diff;
        }
    }

    return closestTime;
}

export function getTodayWeatherForecast(forecasts) {
    const currentDate = getTodayDate();
    const currentForecast = forecasts.find(x => x.date === currentDate);
    return currentForecast;
}

export function getTodayDate() {
    return new Date().toJSON().slice(0, 10);
}