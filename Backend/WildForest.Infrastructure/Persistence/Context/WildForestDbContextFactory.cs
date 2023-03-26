using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WildForest.Infrastructure.Persistence.Context;

public class WildForestDbContextFactory : IDesignTimeDbContextFactory<WildForestDbContext>
{
    public WildForestDbContext CreateDbContext(string[] args)
    {
        var connectionString = args[0];
        
        var optionsBuilder = new DbContextOptionsBuilder<WildForestDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        
        return new WildForestDbContext(optionsBuilder.Options);
    }
    
    /* Commands for migrations:
     * 
     * dotnet ef migrations add InitialCreate --project Backend\WildForest.Infrastructure --
     * "Server=localhost;Database=WildForestDb;Port=5432;User Id=postgres;Password=7409amId"
     *
     * dotnet ef database update --project Backend\WildForest.Infrastructure --
     * "Server=localhost;Database=WildForestDb;Port=5432;User Id=postgres;Password=7409amId"
     */
}