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

export function getTodayDate() {
    return new Date().toLocaleDateString('en-CA');
}

export function getCurrentDateInfo() {
    const currentDate = new Date();
    const day = currentDate.getDate();
    const month = currentDate.getMonth() + 1;
    const year = currentDate.getFullYear();
    const dayOfWeek = currentDate.toLocaleString('en', { weekday: 'long' });
    const hours = currentDate.getHours();
    const minutes = currentDate.getMinutes();

    return `${day}-${month}-${year}, ${dayOfWeek} ${hours}:${minutes}`;
}