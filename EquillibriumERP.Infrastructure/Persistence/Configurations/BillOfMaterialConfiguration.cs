using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class BillOfMaterialConfiguration : IEntityTypeConfiguration<BillOfMaterial>
{
    public void Configure(EntityTypeBuilder<BillOfMaterial> entity)
    {
        entity.ToTable("BillOfMaterials");

        entity.HasMany(b => b.Items)
            .WithOne(i => i.BillOfMaterial)
            .HasForeignKey(i => i.BillOfMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(b => b.Product)
            .WithMany()
            .HasForeignKey(b => b.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}