using System;
using System.Collections.Generic;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class Formulation : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // Versioning is critical in chemical manufacturing
    public string Version { get; set; } = "1.0";

    // Finished product this formulation produces
    public Guid ProductId { get; set; }

    // Standard batch size (e.g. 1000 Liters, 500 Kg)
    public decimal StandardBatchSize { get; set; }

    // Navigation
    public ICollection<FormulationItem> Items { get; set; } = new List<FormulationItem>();
}