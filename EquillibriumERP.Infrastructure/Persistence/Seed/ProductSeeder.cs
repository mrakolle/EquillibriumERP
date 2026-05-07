using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class ProductSeeder
{
    public static void Seed(TenantDbContext context)
    {
        if (context.Products.Any())
            return;

        var products = new List<Product>
        {
            new()
            {
                Name = "5L Dishwashing Liquid",
                //Description = "Blue dishwashing liquid",
                SKU = "DW-5L-001"
            }
        };

        context.Products.AddRange(products);
    }
}