using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class FormulationConfiguration : IEntityTypeConfiguration<Formulation>
{
    public void Configure(EntityTypeBuilder<Formulation> entity)
    {
        entity.ToTable("Formulations");

        entity.HasMany(f => f.Items)
            .WithOne(i => i.Formulation)
            .HasForeignKey(i => i.FormulationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}