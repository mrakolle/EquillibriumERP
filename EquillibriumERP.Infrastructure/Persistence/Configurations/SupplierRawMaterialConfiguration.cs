using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class SupplierRawMaterialConfiguration : IEntityTypeConfiguration<SupplierRawMaterial>
{
    public void Configure(EntityTypeBuilder<SupplierRawMaterial> entity)
    {
        entity.ToTable("SupplierRawMaterial");

        entity.HasKey(x => x.Id);

        entity.HasOne(x => x.Supplier)
            .WithMany(s => s.SupplierRawMaterials)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.RawMaterial)
            .WithMany(r => r.SupplierRawMaterials)
            .HasForeignKey(x => x.RawMaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(x => x.RawMaterialId);

        entity.HasIndex(x => new { x.SupplierId, x.RawMaterialId })
            .IsUnique();
    }
}