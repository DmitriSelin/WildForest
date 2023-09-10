using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Services;
using WildForest.Infrastructure.Authentication;
using WildForest.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Application.Common.Interfaces.Http;
using WildForest.Infrastructure.Http;
using WildForest.Infrastructure.Http.Builders;
using WildForest.Infrastructure.Persistence.DataInitialization;
using System.Text;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Infrastructure.Persistence.UoW;
using WildForest.Infrastructure.Common.Extensions;

namespace WildForest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);

        services.AddHttpClient<IWeatherForecastHttpClient, WeatherForecastHttpClient>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IWeatherForecastBuilder, WeatherForecastBuilder>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<WildForestDbContext>(options =>
            options.UseNpgsql(configuration.GetPostgreSQLConnectionString()));

        services.InitializeData();

        return services;
    }

    private static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();

        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}