using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;
using EquillibriumERP.Infrastructure.Persistence.Seed.Registry;

namespace EquillibriumERP.Infrastructure.Persistence.Seed.Manufacturing;

public class RawMaterialSeeder : ISeeder
{
    public string Name => "RAW_MATERIAL";
    public string Group => SeederGroups.Manufacturing;
    public int Order => 2;

    public async Task SeedAsync(
        TenantDbContext context,
        CancellationToken ct = default)
    {
        if (await context.RawMaterials.AnyAsync(ct))
            return;

        var rawMaterials = new List<RawMaterial>
        {
            new RawMaterial
            {
                Id = Guid.NewGuid(),
                Name = "Sodium Laureth Sulfate",
                Unit = "KG",
                CreatedAt = DateTime.UtcNow
            },
            new RawMaterial
            {
                Id = Guid.NewGuid(),
                Name = "Fragrance Base",
                Unit = "L",
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.RawMaterials.AddRangeAsync(rawMaterials, ct);
    }
}