using System;
using System.Collections.Generic;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class QCTest : BaseEntity
{
    // Link to Batch
    public Guid BatchId { get; set; }
    public Batch Batch { get; set; } = null!;

    // Example: pH, Viscosity, Density
    public string TestName { get; set; } = string.Empty;

    // Spec definition (e.g. 6.5 - 7.5)
    public string Specification { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    // Navigation
    public ICollection<QCResult> Results { get; set; } = new List<QCResult>();
}