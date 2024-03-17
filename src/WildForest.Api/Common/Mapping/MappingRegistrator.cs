using Mapster;
using MapsterMapper;
using System.Reflection;

namespace WildForest.Api.Common.Mapping;

public static class MappingRegistrator
{
    private const string applicationAssemblyName = "WildForest.Application";

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies()
            .SingleOrDefault(assembly => assembly.GetName().Name == applicationAssemblyName);

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        config.Scan(applicationAssembly!);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}