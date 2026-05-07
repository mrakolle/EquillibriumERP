using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class BillOfMaterialItemConfiguration : IEntityTypeConfiguration<BillOfMaterialItem>
{
    public void Configure(EntityTypeBuilder<BillOfMaterialItem> modelBuilder)
    {
        modelBuilder.HasKey(x => x.Id);

        // Quantity rules
        modelBuilder.Property(x => x.Quantity)
            .IsRequired()
            .HasPrecision(18, 4);

        modelBuilder.Property(x => x.UnitOfMeasure)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Property(x => x.WastagePercent)
            .HasPrecision(5, 2);

        // BOM → Items (FK)
        modelBuilder.HasOne(x => x.BillOfMaterial)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.BillOfMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        // RawMaterial link
        modelBuilder.HasOne(x => x.RawMaterial)
            .WithMany()
            .HasForeignKey(x => x.RawMaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        // Safety index (prevents duplicate raw material lines per BOM)
        modelBuilder.HasIndex(x => new { x.BillOfMaterialId, x.RawMaterialId })
            .IsUnique();
    }
}