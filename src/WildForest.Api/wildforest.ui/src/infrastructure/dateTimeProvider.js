const currentHour = () => {
    return new Date().getHours();
}

export function getClosestTime(times) {
    let closestTime = times[0];
    let timeDiff = Math.abs(currentHour() - parseInt(closestTime));

    for (let i = 0; i < times.length; i++) {
        const diff = Math.abs(currentHour() - parseInt(times[i]));

        if (diff < timeDiff) {
            closestTime = times[i];
            timeDiff = diff;
        }
    }

    return closestTime;
}