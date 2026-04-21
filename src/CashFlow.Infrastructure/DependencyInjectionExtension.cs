using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAcess;
using CashFlow.Infrastructure.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        AddRepositories(services);
        AddDbContext(services, config);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var rawConnectionString = config.GetConnectionString("Connection")
            ?? throw new InvalidOperationException("Connection string 'Connection' não foi encontrada.");

        var connectionStringBuilder = new MySqlConnectionStringBuilder(rawConnectionString);

        if (string.IsNullOrWhiteSpace(connectionStringBuilder.Database))
        {
            connectionStringBuilder.Database = "cashflowdb";
        }

        var connectionString = connectionStringBuilder.ConnectionString;
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 45));

        services.AddDbContext<CashFlowDbContext>(options =>
            options.UseMySql(connectionString, serverVersion, mysql => mysql.EnableRetryOnFailure()));
    }
}
