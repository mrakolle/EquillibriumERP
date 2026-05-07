using EquillibriumERP.Infrastructure.Data;
namespace EquillibriumERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
public class SchemaInitializer
{

    private readonly TenantDbContext _db;

    public SchemaInitializer(TenantDbContext db)
    {
        _db = db;
    }
    public async Task EnsureSchemaExists(string schema)
    {
        await _db.Database.ExecuteSqlAsync($"...");
        /*await _db.Database.ExecuteSqlRawAsync(
            $"CREATE SCHEMA IF NOT EXISTS {schema}");*/
    }
}