using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class QCApprovalConfiguration : IEntityTypeConfiguration<QCApproval>
{
    public void Configure(EntityTypeBuilder<QCApproval> entity)
    {
        entity.ToTable("QCApprovals");

        entity.HasKey(a => a.Id);
    }
}