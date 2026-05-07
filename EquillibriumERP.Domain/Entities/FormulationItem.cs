using System;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class FormulationItem : BaseEntity
{
    // FK → Formulation
    public Guid FormulationId { get; set; }
    public Formulation Formulation { get; set; } = null!;

    // Raw material reference (will later link to procurement/public data)
    public Guid RawMaterialId { get; set; }

    // Quantity required for the standard batch
    public decimal Quantity { get; set; }

    public string Unit { get; set; } = string.Empty;

    // Chemical tolerance (VERY important in real manufacturing)
    public decimal? ToleranceMin { get; set; }
    public decimal? ToleranceMax { get; set; }
}