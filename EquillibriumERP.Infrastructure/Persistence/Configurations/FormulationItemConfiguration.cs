using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class FormulationItemConfiguration : IEntityTypeConfiguration<FormulationItem>
{
    public void Configure(EntityTypeBuilder<FormulationItem> entity)
    {
        entity.ToTable("FormulationItems");
    }
}