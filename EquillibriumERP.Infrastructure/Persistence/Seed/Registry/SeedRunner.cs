// dotnet run seed --all
// dotnet run seed --group CORE
// dotnet run seed --name BOM
namespace EquillibriumERP.Infrastructure.Persistence.Seed.Registry;
public class SeedRunner
{
    private readonly SeederRegistry _registry;
    private readonly TenantDbContext _context;

    public SeedRunner(SeederRegistry registry, TenantDbContext context)
    {
        _registry = registry;
        _context = context;
    }

    public Task RunAll() => _registry.RunAllAsync(_context);

    public Task RunGroup(string group) => _registry.RunGroupAsync(group, _context);

    public Task RunSingle(string name) => _registry.RunAsync(name, _context);
}