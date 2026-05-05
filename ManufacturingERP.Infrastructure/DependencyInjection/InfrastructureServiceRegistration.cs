using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ManufacturingERP.Infrastructure.Persistence; 
using ManufacturingERP.Infrastructure.MultiTenancy;
using ManufacturingERP.Application.Common.Interfaces;
using ManufacturingERP.Infrastructure.Persistence.Seed;
using Hangfire;
using Hangfire.PostgreSql;

namespace ManufacturingERP.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MasterDb");

        // PUBLIC DB
        services.AddDbContext<PublicDbContext>(options =>
            options.UseNpgsql(connectionString));

        // TENANT PROVIDER
        services.AddScoped<ITenantProvider, TenantProvider>();

        // ❌ IMPORTANT: NO AddDbContext<TenantDbContext>

        // TENANT FACTORY
        services.AddScoped<TenantDbContextFactory>();

        // TENANT PROVISIONING
        services.AddScoped<TenantProvisioningService>();

        // SEEDER
        services.AddScoped<IDataSeeder, MasterDataSeeder>();

        // HANGFIRE
        services.AddHangfire(cfg =>
        {
            cfg.UsePostgreSqlStorage(opt =>
            {
                opt.UseNpgsqlConnection(connectionString);
            });
        });

        services.AddHangfireServer();

        return services;
    }
}