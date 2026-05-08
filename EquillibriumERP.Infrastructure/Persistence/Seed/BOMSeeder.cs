using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class BOMSeeder
{
    public static async Task SeedAsync(TenantDbContext context)
    {
        var existingCount = await context.BillOfMaterials.CountAsync();

        if (existingCount > 0)
            return;

        var product = await context.Products.FirstOrDefaultAsync();
        var rawMaterial = await context.RawMaterials.FirstOrDefaultAsync();

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

        await context.BillOfMaterials.AddAsync(bom);
        await context.BillOfMaterialItems.AddAsync(bomItem);

        await context.SaveChangesAsync();
    }
}