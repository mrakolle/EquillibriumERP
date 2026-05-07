using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;
public class BOM : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}