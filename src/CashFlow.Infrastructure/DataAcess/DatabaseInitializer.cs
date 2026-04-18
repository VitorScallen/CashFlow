using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CashFlow.Infrastructure.DataAcess;

namespace CashFlow.Infrastructure;

public static class DatabaseInitializer
{
    public static void InitializeDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();

        var hasMigrations = dbContext.Database.GetMigrations().Any();

        if (hasMigrations)
        {
            dbContext.Database.Migrate();
            return;
        }

        dbContext.Database.EnsureCreated();
    }
}
