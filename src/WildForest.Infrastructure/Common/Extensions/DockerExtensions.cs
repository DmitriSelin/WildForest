using Microsoft.Extensions.Configuration;

namespace WildForest.Infrastructure.Common.Extensions;

public static class DockerExtensions
{
    public static string GetConnectionStringFromEnvironment(this IConfiguration configuration)
    {
        bool isAppInDocker = IsAppInDocker();
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        if (!isAppInDocker)
            connectionString = connectionString.Replace("postgres_db", "localhost");

        return connectionString;
    }

    public static bool IsAppInDocker()
    {
        var isRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");

        if (!string.IsNullOrEmpty(isRunningInContainer))
            return true;

        return false;
    }
}