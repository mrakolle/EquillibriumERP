using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class TenantDatabaseSeeder
{
    public static async Task SeedAsync(TenantDbContext context)
    {
        try
        {
            await SeedSuppliers(context);
            await SeedRawMaterials(context);
            await SeedProducts(context);
            await SeedBOMs(context);

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // IMPORTANT: prevents silent failures
            throw new Exception($"Seeding failed: {ex.Message}", ex);
        }
    }

    private static async Task SeedSuppliers(TenantDbContext context)
    {
        if (await context.Suppliers.AnyAsync())
            return;

        await SupplierSeeder.SeedAsync(context);
    }

    private static async Task SeedRawMaterials(TenantDbContext context)
    {
        if (await context.RawMaterials.AnyAsync())
            return;

        await RawMaterialSeeder.SeedAsync(context);
    }

    private static async Task SeedProducts(TenantDbContext context)
    {
        if (await context.Products.AnyAsync())
            return;

        await ProductSeeder.SeedAsync(context);
    }

    private static async Task SeedBOMs(TenantDbContext context)
    {
        if (await context.BillOfMaterials.AnyAsync())
            return;

        await BOMSeeder.SeedAsync(context);
    }
}