using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EquillibriumERP.Infrastructure.Persistence;

public class TenantDbContext : DbContext
{
    private readonly string _schema;

    public string Schema => _schema;

    public TenantDbContext(DbContextOptions<TenantDbContext> options, string schema)
        : base(options)
    {
         EfContextGuard.Validate(this);
        _schema = schema;
        //_schema = schema ?? throw new ArgumentNullException(nameof(schema));
        Console.WriteLine($"🚨 DbContext CREATED WITH SCHEMA: {_schema}");
        
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Inventory> Inventory => Set<Inventory>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<RawMaterial> RawMaterials => Set<RawMaterial>();
    public DbSet<BillOfMaterial> BillOfMaterials => Set<BillOfMaterial>();
    public DbSet<BillOfMaterialItem> BOMItems => Set<BillOfMaterialItem>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
  
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .ReplaceService<IModelCacheKeyFactory, TenantModelCacheKeyFactory>()
            .ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    base.OnModelCreating(modelBuilder);

        // 🔥 CALL YOUR CONFIG METHODS (THIS IS WHAT WAS MISSING)
        ConfigureFormulation(modelBuilder);
        ConfigureWorkOrder(modelBuilder);
        ConfigureBatch(modelBuilder);
        ConfigureQC(modelBuilder);
        ConfigureInventory(modelBuilder);
        ConfigureBillOfMaterial(modelBuilder);
        ConfigureBillOfMaterialItem(modelBuilder);
        
    }
    
    private void ConfigureBillOfMaterial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BillOfMaterial>(entity =>
        {
            entity.ToTable("BillOfMaterials");

            entity.HasMany(b => b.Items)
                .WithOne(i => i.BillOfMaterial)
                .HasForeignKey(i => i.BillOfMaterialId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.Product)
                .WithMany()
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
    private void ConfigureBillOfMaterialItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BillOfMaterialItem>(entity =>
        {
            entity.ToTable("BillOfMaterialItems");

            entity.Property(x => x.Quantity)
                .HasPrecision(18, 4);

            entity.Property(x => x.WastagePercent)
                .HasPrecision(5, 2);

            entity.HasOne(i => i.RawMaterial)
                .WithMany()
                .HasForeignKey(i => i.RawMaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(i => new { i.BillOfMaterialId, i.RawMaterialId })
                .IsUnique();
        });
    }
    private void ConfigureFormulation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Formulation>(entity =>
        {
            entity.ToTable("Formulations");

            entity.HasMany(f => f.Items)
                .WithOne(i => i.Formulation)
                .HasForeignKey(i => i.FormulationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FormulationItem>(entity =>
        {
            entity.ToTable("FormulationItems");
        });
    }
    private void ConfigureWorkOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.ToTable("WorkOrders");

            entity.HasOne(w => w.Formulation)
                .WithMany()
                .HasForeignKey(w => w.FormulationId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureBatch(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.ToTable("Batches");

            entity.HasOne(b => b.WorkOrder)
                .WithMany(w => w.Batches)
                .HasForeignKey(b => b.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BatchConsumption>(entity =>
        {
            entity.ToTable("BatchConsumptions");

            entity.HasOne(c => c.Batch)
                .WithMany(b => b.Consumptions)
                .HasForeignKey(c => c.BatchId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductionRecord>(entity =>
        {
            entity.ToTable("ProductionRecords");

            entity.HasOne(p => p.Batch)
                .WithMany(b => b.ProductionRecords)
                .HasForeignKey(p => p.BatchId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureQC(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QCTest>(entity =>
        {
            entity.ToTable("QCTests");

            entity.HasOne(t => t.Batch)
                .WithMany(b => b.QCTests)
                .HasForeignKey(t => t.BatchId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QCResult>(entity =>
        {
            entity.ToTable("QCResults");

            entity.HasOne(r => r.QCTest)
                .WithMany(t => t.Results)
                .HasForeignKey(r => r.QCTestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QCApproval>(entity =>
        {
            entity.ToTable("QCApprovals");

            entity.HasKey(a => a.Id);
        });
    }

    private void ConfigureInventory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.HasKey(i => i.Id);
        });
    }
}

internal class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        var tenantContext = (TenantDbContext)context;
        return (context.GetType(), tenantContext.Schema, designTime);
    }
}