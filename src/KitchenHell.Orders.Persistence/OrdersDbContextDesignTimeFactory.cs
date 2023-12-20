using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KitchenHell.Orders.Persistence;

/// <summary>
/// For <c>dotnet ef </c> commands.
/// </summary>
internal class OrdersDbContextDesignTimeFactory : IDesignTimeDbContextFactory<OrdersDbContext>
{
    public OrdersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>();
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

        return new OrdersDbContext(optionsBuilder.Options);
    }
}
