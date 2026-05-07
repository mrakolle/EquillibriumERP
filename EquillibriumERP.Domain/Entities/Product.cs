using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; } = "";
    public string SKU { get; set; } = "";
    public string Type { get; set; } = "FinishedGoods";
}