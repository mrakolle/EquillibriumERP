using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

    public class SupplierRawMaterial : BaseEntity
{
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public Guid RawMaterialId { get; set; }
    public RawMaterial RawMaterial { get; set; } = null!;

    public decimal UnitPrice { get; set; }
    public int LeadTimeDays { get; set; }
}
