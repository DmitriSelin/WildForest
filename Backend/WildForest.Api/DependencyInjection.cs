using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using Quartz;
using Swashbuckle.AspNetCore.Filters;
using WildForest.Api.BackgroundServices;
using WildForest.Api.Common.Errors;
using WildForest.Api.Common.Mapping;
using WildForest.Api.Services.Http.Jwt;
using WildForest.Api.Services.Http.Weather;
using WildForest.Api.Services.Weather;

namespace WildForest.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwagger();
            services.AddQuartzToDI();

            services.AddSingleton<ProblemDetailsFactory, WildForestProblemDetailsFactory>();
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            services.AddMappings();
            services.AddTransient<IJwtTokenDecoder, JwtTokenDecoder>();
            services.AddHttpClient<IWeatherForecastHttpClient, WeatherForecastHttpClient>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            return services;
        }
        
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer scheme", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer authorization scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            
            return services;
        }

        private static IServiceCollection AddQuartzToDI(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = new JobKey("WeatherDetectorJob");
                q.AddJob<WeatherDetectorJob>(options => options.WithIdentity(jobKey));

                q.AddTrigger(options => options
                    .ForJob(jobKey)
                    .WithIdentity("WeatherDetectorJob-trigger")
                    .WithCronSchedule("10 0 0 */5 * ?"));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            
            return services;
        }
    }
}