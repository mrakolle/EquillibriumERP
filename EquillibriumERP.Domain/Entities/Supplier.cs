using System;
using System.Collections.Generic;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class Supplier : BaseEntity
{
    
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public string RegistrationNumber { get; set; } = null!;

    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public ICollection<SupplierRawMaterial> SupplierRawMaterials { get; set; }
    = new List<SupplierRawMaterial>();
}