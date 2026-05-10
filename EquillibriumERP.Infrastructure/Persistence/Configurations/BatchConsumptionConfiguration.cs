using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class BatchConsumptionConfiguration : IEntityTypeConfiguration<BatchConsumption>
{
    public void Configure(EntityTypeBuilder<BatchConsumption> entity)
    {
        entity.ToTable("BatchConsumptions");

        entity.HasOne(c => c.Batch)
            .WithMany(b => b.Consumptions)
            .HasForeignKey(c => c.BatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}