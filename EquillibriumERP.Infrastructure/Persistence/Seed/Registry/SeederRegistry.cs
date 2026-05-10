using EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;

namespace EquillibriumERP.Infrastructure.Persistence.Seed.Registry;
public class SeederRegistry
{
    private readonly IEnumerable<ISeeder> _seeders;

    public SeederRegistry(IEnumerable<ISeeder> seeders)
    {
        _seeders = seeders;
    }

    public async Task RunAllAsync(
        TenantDbContext context,
        CancellationToken ct = default)
    {
        foreach (var seeder in _seeders.OrderBy(s => s.Order))
        {
            await seeder.SeedAsync(context, ct);
        }
    }

    public async Task RunAsync(
        string name,
        TenantDbContext context,
        CancellationToken ct = default)
    {
        var seeder = _seeders.FirstOrDefault(s => s.Name == name);

        if (seeder == null)
            throw new Exception($"Seeder '{name}' not found");

        await seeder.SeedAsync(context, ct);
    }

    public async Task RunGroupAsync(
        string group,
        TenantDbContext context,
        CancellationToken ct = default)
    {
        var seeders = _seeders
            .Where(s => s.Group == group)
            .OrderBy(s => s.Order);

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(context, ct);
        }
    }
}