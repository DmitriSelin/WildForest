using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Authentication.Commands.RefreshTokens;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Commands.RevokeTokens;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Common.Mapping;
using WildForest.Application.Maps.Commands.AddCities;
using WildForest.Application.Maps.Commands.AddCountry;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Application.Marks.Commands.LeaveComment;
using WildForest.Application.Marks.Commands.PutRating;
using WildForest.Application.Marks.Queries.GetComments;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

namespace WildForest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserRegistrator, UserRegistrator>();
        services.AddScoped<IUserLogger, UserLogger>();
        services.AddScoped<IRefreshTokenCommandHandler, RefreshTokenCommandHandler>();
        services.AddScoped<IRevokeTokenCommandHandler, RevokeTokenCommandHandler>();
        services.AddScoped<ICountriesListQueryHandler, CountriesListQueryHandler>();
        services.AddScoped<ICitiesListQueryHandler, CitiesListQueryHandler>();
        services.AddScoped<IHomeWeatherForecastHandler, HomeWeatherForecastHandler>();
        services.AddScoped<ICountryCommandHandler, CountryCommandHandler>();
        services.AddScoped<ICityCommandHandler, CityCommandHandler>();
        services.AddScoped<ICommentCommandHandler, CommentCommandHandler>();
        services.AddScoped<IRatingCommandHandler, RatingCommandHandler>();
        services.AddScoped<ICommentsQueryHandler, CommentsQueryHandler>();

        services.AddMappings();

        return services;
    }
}