using System;
using System.Collections.Generic;
using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;

public class Branch : BaseEntity
{
    
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string Address { get; set; } = null!;

    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
}