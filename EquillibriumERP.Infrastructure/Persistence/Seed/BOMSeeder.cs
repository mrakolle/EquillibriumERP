using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class BOMSeeder
{
    public static void Seed(TenantDbContext context)
    {
        if (context.BillOfMaterials.Any())
            return;

        var product = context.Products.FirstOrDefault();
        var materials = context.RawMaterials.ToList();

        if (product == null || !materials.Any())
            return;

        var bom = CreateDishwashingLiquidBOM(product.Id, materials);

        context.BillOfMaterials.Add(bom);
    }

    private static BillOfMaterial CreateDishwashingLiquidBOM(Guid productId, List<RawMaterial> materials)
    {
        RawMaterial Get(string name) =>
            materials.First(x => x.RawMaterialMaster.Name == name);

        var sles = Get("SLES");
        var capb = Get("CAPB");
        var salt = Get("Sodium Chloride");
        var fragrance = Get("Fragrance");
        var dye = Get("Blue Dye");
        var preservative = Get("Preservative");
        var water = Get("Water");

        return new BillOfMaterial
        {
            ProductId = productId,
            BOMCode = "BOM-DW-5L-001",
            Name = "5L Dishwashing Liquid - Blue Formula",
            Version = "1.0",
            IsActive = true,
            EffectiveDate = DateTime.UtcNow,

            Items = new List<BillOfMaterialItem>
            {
                new() { RawMaterialId = sles.Id, Quantity = 0.8m, UnitOfMeasure = "kg", WastagePercent = 1 },
                new() { RawMaterialId = capb.Id, Quantity = 0.5m, UnitOfMeasure = "kg", WastagePercent = 1 },
                new() { RawMaterialId = salt.Id, Quantity = 0.2m, UnitOfMeasure = "kg", WastagePercent = 0 },
                new() { RawMaterialId = fragrance.Id, Quantity = 0.05m, UnitOfMeasure = "kg", WastagePercent = 2 },
                new() { RawMaterialId = dye.Id, Quantity = 0.01m, UnitOfMeasure = "kg", WastagePercent = 0 },
                new() { RawMaterialId = preservative.Id, Quantity = 0.02m, UnitOfMeasure = "kg", WastagePercent = 0 },
                new() { RawMaterialId = water.Id, Quantity = 3.42m, UnitOfMeasure = "kg", WastagePercent = 0 }
            }
        };
    }
}