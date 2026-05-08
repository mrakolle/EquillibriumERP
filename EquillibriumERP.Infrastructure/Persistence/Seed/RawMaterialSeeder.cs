using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence.Seed;

public static class RawMaterialSeeder
{
    public static async Task SeedAsync(TenantDbContext context)
    {
        var existingCount = await context.RawMaterials.CountAsync();

        if (existingCount > 0)
            return;

        var materials = new List<RawMaterial>
        {
            new RawMaterial
            {
                Id = Guid.NewGuid(),
                CASNumber = "68585-34-2",
                SDSAttachmentPath = "sds/sles.pdf",
                CurrentCost = 25.50m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },

            new RawMaterial
            {
                Id = Guid.NewGuid(),
                CASNumber = "9004-82-4",
                SDSAttachmentPath = "sds/cdea.pdf",
                CurrentCost = 18.75m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.RawMaterials.AddRangeAsync(materials);

        await context.SaveChangesAsync();
    }
}