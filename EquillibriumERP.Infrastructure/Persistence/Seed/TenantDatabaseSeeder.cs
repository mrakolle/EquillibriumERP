

namespace EquillibriumERP.Infrastructure.Persistence.Seed;
public static class TenantDatabaseSeeder
{
    public static void Seed(TenantDbContext context)
    {
        SeedRawMaterials(context);
        SeedBOMs(context);
        //SeedInventory(context);

        context.SaveChanges();
    }
    private static void SeedRawMaterials(TenantDbContext context)
    {
        if (context.RawMaterials.Any())
            return;

        RawMaterialSeeder.Seed(context);
    }

    private static void SeedProducts(TenantDbContext context)
    {
        if (context.Products.Any())
            return;

        ProductSeeder.Seed(context);
    }

    private static void SeedBOMs(TenantDbContext context)
    {
        if (context.BillOfMaterials.Any())
            return;

        BOMSeeder.Seed(context);
    }
}
