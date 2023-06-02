namespace WildForest.Frontend.Resources.ItemKeys;

internal class WeatherImage
{
    internal static string GetWeatherImage(string weatherName)
    {
        switch (weatherName)
        {
            case "":
                return "../Resources/Images/Icons/sunny.png";
        }

        return "";
    }
}