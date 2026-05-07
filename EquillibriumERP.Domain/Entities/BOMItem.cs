using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;
public class BOMItem : BaseEntity
{
    public Guid BOMId { get; set; }
    public Guid ComponentProductId { get; set; }

    public decimal Quantity { get; set; }
}