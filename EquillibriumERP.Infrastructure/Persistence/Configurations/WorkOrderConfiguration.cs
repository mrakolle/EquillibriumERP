using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> entity)
    {
        entity.ToTable("WorkOrders");

        entity.HasOne(w => w.Formulation)
            .WithMany()
            .HasForeignKey(w => w.FormulationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}