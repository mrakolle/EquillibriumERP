using System;
using EquillibriumERP.Domain.Common;
using EquillibriumERP.Domain.Enums;
using System.Collections.Generic;

namespace EquillibriumERP.Domain.Entities;

public class Batch : BaseEntity
{
    // Link to planning
    public Guid WorkOrderId { get; set; }
    public WorkOrder WorkOrder { get; set; } = null!;

    // Unique batch identifier (important for traceability)
    public string BatchNumber { get; set; } = string.Empty;

    // Planned vs actual production
    public decimal PlannedQuantity { get; set; }
    public decimal ActualQuantity { get; set; }

    // Timing
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    // Status
    public BatchStatus Status { get; set; }

    public ICollection<BatchConsumption> Consumptions { get; set; } = new List<BatchConsumption>();
    public ICollection<ProductionRecord> ProductionRecords { get; set; } = new List<ProductionRecord>();
    public ICollection<QCTest> QCTests { get; set; } = new List<QCTest>();
}