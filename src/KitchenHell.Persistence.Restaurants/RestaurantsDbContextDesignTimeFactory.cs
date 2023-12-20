using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KitchenHell.Persistence.Restaurants;

/// <summary>
/// For <c>dotnet ef </c> commands.
/// </summary>
public class RestaurantsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<RestaurantsDbContext>
{
    public RestaurantsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RestaurantsDbContext>();
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString(DatabaseConfigurations.DatabaseName);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = "DataSource=dummy";
        }

        optionsBuilder.UseOrdersDatabase(connectionString);

        return new RestaurantsDbContext(optionsBuilder.Options);
    }
}
