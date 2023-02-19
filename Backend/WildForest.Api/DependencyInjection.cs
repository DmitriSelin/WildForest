using Microsoft.AspNetCore.Mvc.Infrastructure;
using WildForest.Api.Common.Errors;
using WildForest.Api.Common.Mapping;
using WildForest.Api.Services.Http.Jwt;
using WildForest.Api.Services.Http.Weather;

namespace WildForest.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<ProblemDetailsFactory, WildForestProblemDetailsFactory>();
            services.AddMappings();
            services.AddTransient<IJwtTokenDecoder, JwtTokenDecoder>();
            services.AddHttpClient<IWeatherForecastHttpClient, WeatherForecastHttpClient>();

            return services;
        }
    }
}