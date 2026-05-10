using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class ProductionRecordConfiguration : IEntityTypeConfiguration<ProductionRecord>
{
    public void Configure(EntityTypeBuilder<ProductionRecord> entity)
    {
        entity.ToTable("ProductionRecords");

        entity.HasOne(p => p.Batch)
            .WithMany(b => b.ProductionRecords)
            .HasForeignKey(p => p.BatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}