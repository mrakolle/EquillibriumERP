using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Persistence;

namespace EquillibriumERP.Infrastructure.MultiTenancy;

public class TenantProvisioningService
{
    private readonly TenantDbContextFactory _factory;
    private readonly MasterDbContext _publicDb;

    public TenantProvisioningService(
        TenantDbContextFactory factory,
        MasterDbContext publicDb)
    {
        _factory = factory;
        _publicDb = publicDb;
    }

    public async Task<Tenant> CreateTenantAsync(string tenantName)
    {
        // 1. Generate schema name
        var tenantId = Guid.NewGuid().ToString("N");
        var tenantSchema = $"tenant_{tenantId}";

        // 2. Create tenant record (PUBLIC DB)
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = tenantName,
            Schema = tenantSchema,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _publicDb.Tenants.Add(tenant);
        await _publicDb.SaveChangesAsync();

        // 3. Create tenant context
        await using var context = _factory.Create(tenantSchema);

        // 4. CREATE SCHEMA
        await context.Database.ExecuteSqlRawAsync(
            $"CREATE SCHEMA IF NOT EXISTS \"{tenantSchema}\"");

        // 🔥 5. FORCE CONNECTION INTO SCHEMA (CRITICAL FIX)
        var conn = context.Database.GetDbConnection();

        if (conn.State != System.Data.ConnectionState.Open)
            await conn.OpenAsync();

       /* using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = $"SET search_path TO \"{tenantSchema}\", public";
            await cmd.ExecuteNonQueryAsync();
        }*/

        Console.WriteLine($"🚀 MIGRATING INTO SCHEMA: {tenantSchema}");

        // 6. APPLY MIGRATIONS (NOW GUARANTEED CORRECT SCHEMA)
        await context.Database.MigrateAsync();

        return tenant;
    }
}
