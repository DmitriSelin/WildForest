using Microsoft.Extensions.Configuration;

namespace WildForest.Infrastructure.Common.Extensions;

public static class ConfigurationExtensions
{
    public static string GetPostgreSQLConnectionString(this ConfigurationManager configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("PostgresConnectionString");

        if (connectionString == null)
            throw new ArgumentNullException(nameof(connectionString), "PostgresConnectionString is null");

        return connectionString;
    }
}