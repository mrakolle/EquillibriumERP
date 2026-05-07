using System;
using System.Collections.Generic;
using EquillibriumERP.Domain.Common;
using EquillibriumERP.Domain.Enums;

namespace EquillibriumERP.Domain.Entities;

public class WorkOrder : BaseEntity
{
    // What are we producing
    public Guid ProductId { get; set; }

    // Which formulation (recipe) to use
    public Guid FormulationId { get; set; }
    public Formulation Formulation { get; set; } = null!;

    // 🔥 CRITICAL: link to Sales (demand-driven manufacturing)
    public Guid? SalesOrderId { get; set; }

    // Planned production quantity
    public decimal PlannedQuantity { get; set; }

    // Status lifecycle
    public WorkOrderStatus Status { get; set; }

    // Navigation → a work order can have multiple batches
    public ICollection<Batch> Batches { get; set; } = new List<Batch>();
}