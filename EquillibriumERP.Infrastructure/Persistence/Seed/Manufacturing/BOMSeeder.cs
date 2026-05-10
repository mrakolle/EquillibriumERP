using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;
using EquillibriumERP.Infrastructure.Persistence.Seed.Registry;

namespace EquillibriumERP.Infrastructure.Persistence.Seed.Manufacturing;

public class BomSeeder : ISeeder
{
    public string Name => "BOM";
    public string Group => SeederGroups.Manufacturing;
    public int Order => 5;

    public async Task SeedAsync(
        TenantDbContext context,
        CancellationToken ct = default)
    {
        // Prevent duplicate seeding
        var exists = await context.BillOfMaterials
            .AnyAsync(ct);

        if (exists)
            return;

        var product = await context.Products.FirstOrDefaultAsync(ct);
        var rawMaterial = await context.RawMaterials.FirstOrDefaultAsync(ct);

        if (product == null || rawMaterial == null)
            return;

        var bom = new BillOfMaterial
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            BOMCode = "BOM-001",
            Name = "Dishwashing Liquid Formula",
            Version = "1.0",
            IsActive = true,
            EffectiveDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        var bomItem = new BillOfMaterialItem
        {
            Id = Guid.NewGuid(),
            BillOfMaterialId = bom.Id,
            RawMaterialId = rawMaterial.Id,
            Quantity = 10,
            UnitOfMeasure = "KG",
            WastagePercent = 2,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await context.BillOfMaterials.AddAsync(bom, ct);
        await context.BillOfMaterialItems.AddAsync(bomItem, ct);

        await context.SaveChangesAsync(ct);
    }
}