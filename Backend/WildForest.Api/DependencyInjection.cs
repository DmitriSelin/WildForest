using Microsoft.AspNetCore.Mvc.Infrastructure;
using WildForest.Api.Common.Errors;
using WildForest.Api.Common.Mapping;

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

            return services;
        }
    }
}