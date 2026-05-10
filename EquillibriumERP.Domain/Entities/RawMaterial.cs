using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class RawMaterial : BaseEntity
{
     public string Name { get; set; } = string.Empty;    
     public string CASNumber { get; set; } = string.Empty;
    public string SDSAttachmentPath { get; set; } = string.Empty;
    public decimal CurrentCost { get; set; }
    public string Unit { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<SupplierRawMaterial> SupplierRawMaterials { get; set; }
        = new List<SupplierRawMaterial>();
}