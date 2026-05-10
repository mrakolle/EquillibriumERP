using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class BatchConfiguration : IEntityTypeConfiguration<Batch>
{
    public void Configure(EntityTypeBuilder<Batch> entity)
    {
        entity.ToTable("Batches");

        entity.HasOne(b => b.WorkOrder)
            .WithMany(w => w.Batches)
            .HasForeignKey(b => b.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}