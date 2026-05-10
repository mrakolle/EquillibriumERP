using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;
using EquillibriumERP.Infrastructure.Persistence.Seed.Registry;

namespace EquillibriumERP.Infrastructure.Persistence.Seed.Manufacturing;

public class SupplierSeeder : ISeeder
{
    public string Name => "Suppliers";
    public string Group => "Manufacturing";
    public int Order => 1;

    public async Task SeedAsync(
        TenantDbContext context,
        CancellationToken ct = default)
    {
        if (await context.Suppliers.AnyAsync(ct))
            return;

        var suppliers = new List<Supplier>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "EzeeChem",
                Email = "sales@ezeechem.co.za",
                Phone = "+27 11 555 1234",
                RegistrationNumber = "2018/123456/07",
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Suppliers.AddRangeAsync(suppliers, ct);

        await context.SaveChangesAsync(ct);
    }
}