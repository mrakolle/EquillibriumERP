using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class RawMaterialSeeder
{
    public static void Seed(TenantDbContext context)
    {
        if (context.RawMaterials.Any())
            return;

        var materials = new List<RawMaterial>
        {
            new()
            {
                RawMaterialMaster = context.Set<RawMaterialMaster>()
                    .First(x => x.Name == "SLES")
            },
            new()
            {
                RawMaterialMaster = context.Set<RawMaterialMaster>()
                    .First(x => x.Name == "CAPB")
            },
            new()
            {
                RawMaterialMaster = context.Set<RawMaterialMaster>()
                    .First(x => x.Name == "Sodium Chloride")
            },
            new()
            {
                RawMaterialMaster = context.Set<RawMaterialMaster>()
                    .First(x => x.Name == "Fragrance")
            },
            new()
            {
                RawMaterialMaster = context.Set<RawMaterialMaster>()
                    .First(x => x.Name == "Blue Dye")
            },
            new()
            {
                RawMaterialMaster = context.Set<RawMaterialMaster>()
                    .First(x => x.Name == "Preservative")
            },
            new()
            {
                RawMaterialMaster = context.Set<RawMaterialMaster>()
                    .First(x => x.Name == "Water")
            }
        };

        context.RawMaterials.AddRange(materials);
    }
}