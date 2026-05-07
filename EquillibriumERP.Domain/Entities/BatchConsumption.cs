using System;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class BatchConsumption : BaseEntity
{
    // Link to Batch (execution)
    public Guid BatchId { get; set; }
    public Batch Batch { get; set; } = null!;

    // Raw material reference (from procurement/public)
    public Guid RawMaterialId { get; set; }

    // Planned vs actual usage
    public decimal PlannedQuantity { get; set; }
    public decimal ActualQuantity { get; set; }

    public string Unit { get; set; } = string.Empty;

    //  CRITICAL: supplier lot tracking
    public string LotNumber { get; set; } = string.Empty;
}