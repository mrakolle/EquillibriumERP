using System;
using System.Collections.Generic;
using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;

public class SupplierRawMaterial
{
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public Guid RawMaterialMasterId { get; set; }
    public RawMaterialMaster RawMaterial { get; set; } = null!;

    public decimal PricePerKg { get; set; }
    public string LeadTimeDays { get; set; } = null!;
}