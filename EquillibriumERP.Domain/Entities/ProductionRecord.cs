using System;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class ProductionRecord : BaseEntity
{
    // Link to Batch
    public Guid BatchId { get; set; }
    public Batch Batch { get; set; } = null!;

    // What product was produced
    public Guid ProductId { get; set; }

    // Quantity produced
    public decimal QuantityProduced { get; set; }

    public string Unit { get; set; } = string.Empty;

    // Where it is stored (tank, warehouse, etc.)
    public string StorageLocation { get; set; } = string.Empty;
}