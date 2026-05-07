using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;
public class StockTransaction : BaseEntity
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }

    public string Type { get; set; } = ""; // IN / OUT / ADJUST
    public string Reference { get; set; } = "";
}