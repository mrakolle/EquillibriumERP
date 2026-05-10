using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;

namespace EquillibriumERP.Infrastructure.Persistence.Seed.Manufacturing;

public class SupplierRawMaterialSeeder : ISeeder
{
    public string Name => "SupplierRawMaterials";

    public string Group => "Manufacturing";

    public int Order => 3;

    public async Task SeedAsync(
        TenantDbContext context,
        CancellationToken ct = default)
    {
        if (await context.SupplierRawMaterials.AnyAsync(ct))
            return;

        var suppliers = await context.Suppliers.ToListAsync(ct);

        var rawMaterials = await context.RawMaterials.ToListAsync(ct);

        if (!suppliers.Any() || !rawMaterials.Any())
            return;

        var relationships = new List<SupplierRawMaterial>();

        foreach (var supplier in suppliers)
        {
            foreach (var rawMaterial in rawMaterials.Take(5))
            {
                relationships.Add(new SupplierRawMaterial
                {
                    Id = Guid.NewGuid(),
                    SupplierId = supplier.Id,
                    RawMaterialId = rawMaterial.Id,
                    UnitPrice = 25.50m,
                    LeadTimeDays = 3,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        await context.SupplierRawMaterials
            .AddRangeAsync(relationships, ct);

        await context.SaveChangesAsync(ct);
    }
}