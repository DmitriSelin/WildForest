const weatherMap = new Map();
weatherMap.set('Thunderstorm', 'cloud-bolt');
weatherMap.set('Drizzle', 'cloud-rain');
weatherMap.set('Rain', 'cloud-rain');
weatherMap.set('Snow', 'snowflake');
weatherMap.set('Clear', 'sun');
weatherMap.set('Clouds', 'cloud');
weatherMap.set('Mist', 'smog');
weatherMap.set('Smoke', 'smog');
weatherMap.set('Haze', 'smog');
weatherMap.set('Dust', 'smog');
weatherMap.set('Fog', 'smog');
weatherMap.set('Sand', 'smog');
weatherMap.set('Ash', 'smog');
weatherMap.set('Squall', 'smog');
weatherMap.set('Tornado', 'smog');

export const getIconFromWeatherName = (weatherKey) => {
    const iconName = weatherMap.get(weatherKey);

    if (iconName === undefined)
        return '';

    return iconName;
}