using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class QCResultConfiguration : IEntityTypeConfiguration<QCResult>
{
    public void Configure(EntityTypeBuilder<QCResult> entity)
    {
        entity.ToTable("QCResults");

        entity.HasOne(r => r.QCTest)
            .WithMany(t => t.Results)
            .HasForeignKey(r => r.QCTestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}