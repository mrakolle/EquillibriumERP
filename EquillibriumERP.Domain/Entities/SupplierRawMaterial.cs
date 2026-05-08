using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

    public class SupplierRawMaterial : BaseEntity
{
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public Guid RawMaterialId { get; set; }
    public RawMaterial RawMaterial { get; set; } = null!;

    public decimal PricePerKg { get; set; }
    public string LeadTimeDays { get; set; } = string.Empty;
}
