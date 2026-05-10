namespace EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;
public interface ISeeder
{
    string Name { get; }
    int Order { get; }
    string Group { get; }   // NEW
    Task SeedAsync(TenantDbContext context, CancellationToken ct = default);
}