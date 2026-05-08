using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class SupplierSeeder
{
    public static async Task SeedAsync(TenantDbContext context)
    {
        var existingCount = await context.Suppliers.CountAsync();

        if (existingCount > 0)
            return;

        var suppliers = new List<Supplier>
        {
            new Supplier
            {
                Id = Guid.NewGuid(),
                Name = "EzeeChem",
                Email = "info@ezeechem.co.za",
                Phone = "011 000 0000",
                RegistrationNumber = "SA-EC-001",
                CreatedAt = DateTime.UtcNow
            },

            new Supplier
            {
                Id = Guid.NewGuid(),
                Name = "Raw Detergent Supplies",
                Email = "info@rds.co.za",
                Phone = "011 111 1111",
                RegistrationNumber = "SA-RDS-002",
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Suppliers.AddRangeAsync(suppliers);

        await context.SaveChangesAsync();
    }
}