using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;

namespace EquillibriumERP.Infrastructure.Persistence.Seed.Manufacturing;

public class ProductSeeder : ISeeder
{
    public string Name => "Products";

    public string Group => "Manufacturing";

    public int Order => 3;

    public async Task SeedAsync(
        TenantDbContext context,
        CancellationToken ct = default)
    {
        // Idempotent guard
        if (await context.Products.AnyAsync(ct))
            return;

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dishwashing Liquid",
                SKU = "DWL",
                Type = "Finished Goods",
                CreatedAt = DateTime.UtcNow
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laundry Detergent",
                SKU = "LDRY",
                Type = "Finished Goods",
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Products.AddRangeAsync(products, ct);

        await context.SaveChangesAsync(ct);
    }
}