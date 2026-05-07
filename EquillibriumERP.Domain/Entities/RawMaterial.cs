using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class RawMaterial : BaseEntity
{
    // Foreign Key to RawMaterialMaster
    public Guid RawMaterialMasterId { get; set; }

    public RawMaterialMaster RawMaterialMaster { get; set; } = null!;

    // Operational fields (BOM / usage level)
    public string CASNumber { get; set; } = string.Empty;

    public string SDSAttachmentPath { get; set; } = string.Empty;

    public decimal CurrentCost { get; set; }

    public bool IsActive { get; set; } = true;
}