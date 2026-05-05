using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ManufacturingERP.Infrastructure.MultiTenancy;

namespace ManufacturingERP.Infrastructure.Persistence;

public class TenantDbContextFactory
{
    private readonly IConfiguration _configuration;
    private readonly ITenantProvider _tenantProvider;

    public TenantDbContextFactory(
        IConfiguration configuration,
        ITenantProvider tenantProvider)
    {
        _configuration = configuration;
        _tenantProvider = tenantProvider;
    }

    // 🔥 RUNTIME (normal requests)
    public TenantDbContext Create()
    {
        var schema = _tenantProvider.Schema;

        if (string.IsNullOrWhiteSpace(schema) || schema == "design_time")
            throw new Exception("Invalid runtime tenant resolution");

        var context = new TenantDbContext(BuildOptions(schema), schema);

        SetSchema(context, schema);

        return context;
    }

    // 🔥 PROVISIONING (tenant creation)
    public TenantDbContext Create(string schema)
    {
        if (string.IsNullOrWhiteSpace(schema) || schema == "design_time")
            throw new Exception("Invalid provisioning schema");

        Console.WriteLine($"🔥 FACTORY (PROVISIONING): {schema}");

        var context = new TenantDbContext(BuildOptions(schema), schema);

        SetSchema(context, schema);

        return context;
    }

    // 🔥 MIGRATIONS SAFETY (optional guard)
    public TenantDbContext CreateForMigrations()
    {
        var schema = _tenantProvider.Schema;

        if (schema == "design_time")
            throw new InvalidOperationException("Design-time tenant blocked for migrations");

        if (string.IsNullOrWhiteSpace(schema))
            throw new InvalidOperationException("No tenant resolved");

        var context = new TenantDbContext(BuildOptions(schema), schema);

        SetSchema(context, schema);

        return context;
    }

    // 🔥 CENTRALIZED OPTIONS
    private DbContextOptions<TenantDbContext> BuildOptions(string schema)
    {
        var connectionString = _configuration.GetConnectionString("MasterDb");

        return new DbContextOptionsBuilder<TenantDbContext>()
            .UseNpgsql(connectionString, x =>
            {
                // each tenant has its own migration history
                x.MigrationsHistoryTable("__EFMigrationsHistory", schema);
            })
            .Options;
    }

    // 🔥 THE FIX — FORCE POSTGRES TO USE TENANT SCHEMA
    private static void SetSchema(TenantDbContext context, string schema)
    {
        var conn = context.Database.GetDbConnection();

        if (conn.State != System.Data.ConnectionState.Open)
            conn.Open();

        using var command = conn.CreateCommand();

        // 🔥 critical fix
        command.CommandText = $"SET search_path TO \"{schema}\", public";

        command.ExecuteNonQuery();

        Console.WriteLine($"✅ search_path set to: {schema},public");
    }
    /*private static void SetSchema(TenantDbContext context, string schema)
    {
        context.Database.OpenConnection();

        using var command = context.Database.GetDbConnection().CreateCommand();
        command.CommandText = $"SET search_path TO \"{schema}\"";
        command.ExecuteNonQuery();

        Console.WriteLine($"✅ search_path set to: {schema}");
    }*/
}