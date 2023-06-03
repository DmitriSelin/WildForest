namespace WildForest.Frontend.Resources.ItemKeys;

internal class WeatherImage
{
    internal static string GetWeatherImage(string weatherName)
    {
        switch (weatherName)
        {
            case "Thunderstorm":
                return ReturnFullPath("thunderstorm.png");
            case "Drizzle":
                return ReturnFullPath("drizzle.png");
            case "Rain":
                return ReturnFullPath("rain.png");
            case "Snow":
                return ReturnFullPath("snow.png");
            case "Clear":
                return ReturnFullPath("sunny.png");
            case "Clouds":
                return ReturnFullPath("clouds.png");
            case "Dust":
            case "Sand":
                return ReturnFullPath("sandstorm.png");
            case "Squall":
            case "Tornado":
                return ReturnFullPath("typhoon.png");
            default:
                return "fog.png";
        }
    }

    private static string ReturnFullPath(string imageName)
    {
        return "../Resources/Images/WeatherIcons/" + imageName;
    }
}