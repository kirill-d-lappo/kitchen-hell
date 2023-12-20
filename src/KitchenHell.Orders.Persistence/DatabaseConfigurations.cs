using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KitchenHell.Orders.Persistence;

public static class DatabaseConfigurations
{
    public const string DatabaseName = "Orders";
    public const string Schema = "orders";

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
