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
}