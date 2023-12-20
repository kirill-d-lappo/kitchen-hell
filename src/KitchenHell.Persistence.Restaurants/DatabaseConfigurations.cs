using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KitchenHell.Persistence.Restaurants;

public static class DatabaseConfigurations
{
    public const string DatabaseName = "Restaurants";
    public const string Schema = "restaurants";

    public static void UseOrdersDatabase(
        this DbContextOptionsBuilder optionsBuilder,
        string connectionString = default
    )
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = $"name={DatabaseName}";
        }

        optionsBuilder.UseSqlServer(connectionString, ConfigureRoutesDbContext);
    }

    private static void ConfigureRoutesDbContext(SqlServerDbContextOptionsBuilder b)
    {
        b.MigrationsHistoryTable("__EFMigrationsHistory", Schema);
    }
}
