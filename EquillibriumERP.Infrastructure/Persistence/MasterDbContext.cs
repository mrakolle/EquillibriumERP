using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Infrastructure.Persistence
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }

        // =====================
        // MASTER DATA TABLES
        // =====================
        public DbSet<Tenant> Tenants => Set<Tenant>();

        public DbSet<RawMaterialMaster> RawMaterialMasters => Set<RawMaterialMaster>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<SupplierRawMaterial> SupplierRawMaterials => Set<SupplierRawMaterial>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureTenant(modelBuilder);
            ConfigureRawMaterialMaster(modelBuilder);
            ConfigureSupplier(modelBuilder);
            ConfigureBranch(modelBuilder);
            //ConfigureSupplierRawMaterial(modelBuilder);
        }
        private void ConfigureTenant(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenants", "public");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Schema).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.IsActive).IsRequired();
            });
        }
        private void ConfigureRawMaterialMaster(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RawMaterialMaster>(entity =>
            {
                entity.ToTable("RawMaterialMasters", "public");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);

                entity.HasIndex(x => x.Name).IsUnique();
            });
        }
        private void ConfigureSupplier(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers", "public");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);

                entity.HasIndex(x => x.Name).IsUnique();
            });
        }
        private void ConfigureBranch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branches", "public");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name).IsRequired();

                entity.HasOne(x => x.Supplier)
                    .WithMany(x => x.Branches)
                    .HasForeignKey(x => x.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(x => new { x.SupplierId, x.Name }).IsUnique();
            });
        }
        private void ConfigureSupplierRawMaterial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplierRawMaterial>(entity =>
            {
                entity.ToTable("SupplierRawMaterial");

                entity.HasKey(x => new { x.SupplierId, x.RawMaterialId });

                entity.HasOne(x => x.Supplier)
                    .WithMany(s => s.SupplierRawMaterials)
                    .HasForeignKey(x => x.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.RawMaterial)
                    .WithMany(r => r.SupplierRawMaterials)
                    .HasForeignKey(x => x.RawMaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => new { x.SupplierId, x.RawMaterialId })
                    .IsUnique();
            });
        }
    }
}