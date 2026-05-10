using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Infrastructure.Persistence.Configurations;

public class QCTestConfiguration : IEntityTypeConfiguration<QCTest>
{
    public void Configure(EntityTypeBuilder<QCTest> entity)
    {
        entity.ToTable("QCTests");

        entity.HasOne(t => t.Batch)
            .WithMany(b => b.QCTests)
            .HasForeignKey(t => t.BatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}