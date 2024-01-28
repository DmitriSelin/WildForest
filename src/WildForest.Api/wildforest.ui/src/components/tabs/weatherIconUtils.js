const weatherMap = new Map();
weatherMap.set('Thunderstorm', { name: 'cloud-bolt', priority: 5 });
weatherMap.set('Drizzle', { name: 'cloud-rain', priority: 4 });
weatherMap.set('Rain', { name: 'cloud-rain', priority: 4 });
weatherMap.set('Snow', { name: 'snowflake', priority: 3 });
weatherMap.set('Clear', { name: 'sun', priority: 1 });
weatherMap.set('Clouds', { name: 'cloud', priority: 2 });
weatherMap.set('Mist', { name: 'smog', priority: 0 });
weatherMap.set('Smoke', { name: 'smog', priority: 0 });
weatherMap.set('Haze', { name: 'smog', priority: 0 });
weatherMap.set('Dust', { name: 'smog', priority: 1 });
weatherMap.set('Fog', { name: 'smog', priority: 0 });
weatherMap.set('Sand', { name: 'smog', priority: 1 });
weatherMap.set('Ash', { name: 'smog', priority: 1 });
weatherMap.set('Squall', { name: 'smog', priority: 1 });
weatherMap.set('Tornado', { name: 'smog', priority: 1 });

export const getIconFromWeatherName = (weatherKey) => {
    const icon = weatherMap.get(weatherKey);

    if (icon === undefined)
        return '';

    return icon.name;
}

export const getIconNameWithTopPriority = (weatherForecasts) => {
    const firstWeatherForecastName = weatherForecasts[0].description.name;
    let currentWeatherItem = weatherMap.get(firstWeatherForecastName);
    let iconName = firstWeatherForecastName;

    for (let i = 1; i < weatherForecasts.length; i++) {
        var weatherItem = weatherMap.get(weatherForecasts[i].description.name);

        if (weatherItem.priority > currentWeatherItem.priority) {
            currentWeatherItem = weatherItem;
            iconName = weatherForecasts[i].description.name;
        }
    }

    return iconName;
}