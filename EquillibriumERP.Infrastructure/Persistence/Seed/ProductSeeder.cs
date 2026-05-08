using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class ProductSeeder
{
    public static async Task SeedAsync(TenantDbContext context)
    {
        var existingCount = await context.Products.CountAsync();

        if (existingCount > 0)
            return;

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dishwashing Liquid 5L",
                SKU = "DWL-5L",
                CreatedAt = DateTime.UtcNow
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laundry Detergent 2L",
                SKU = "LDRY-2L",
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Products.AddRangeAsync(products);

        await context.SaveChangesAsync();
    }
}