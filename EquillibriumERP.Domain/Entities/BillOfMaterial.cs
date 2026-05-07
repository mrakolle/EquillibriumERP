using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class BillOfMaterial : BaseEntity
{
    // FK → Product
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string BOMCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Version { get; set; } = "1.0";

    public bool IsActive { get; set; } = true;

    public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;

    public DateTime? ObsoleteDate { get; set; }

    // Navigation
    public virtual ICollection<BillOfMaterialItem> Items { get; set; }
        = new List<BillOfMaterialItem>();
}