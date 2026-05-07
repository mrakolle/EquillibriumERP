using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class BillOfMaterialItem : BaseEntity
{
    // Foreign Key to BOM Header
    public Guid BillOfMaterialId { get; set; }

    public BillOfMaterial BillOfMaterial { get; set; } = null!;

    // Raw material used in this BOM
    public Guid RawMaterialId { get; set; }

    public RawMaterial RawMaterial { get; set; } = null!;

    // Quantity required for this BOM
    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = "kg";

    // Optional: loss factor / wastage
    public decimal WastagePercent { get; set; } = 0;

    public bool IsActive { get; set; } = true;
}