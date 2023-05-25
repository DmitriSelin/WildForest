namespace WildForest.Frontend.Contracts.Weather
{
    public class WeatherResponseBase
    {
        public WeatherForecastVm? WeatherForecast { get; set; }

        public int StatusCode { get; set; }

        public string? Title { get; set; }

        public WeatherResponseBase(WeatherForecastVm? weatherForecast, int statusCode, string? title)
        {
            WeatherForecast = weatherForecast;
            StatusCode = statusCode;
            Title = title;
        }
    }
}