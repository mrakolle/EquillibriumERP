using System;
using System.Collections.Generic;
using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;

public class RawMaterialMaster : BaseEntity
{
    
    public string Name { get; set; } = null!;
    public string CASNumber { get; set; }
    public string Category { get; set; } = null!; // e.g. Surfactant, Solvent, Builder
    public string ChemicalFormula { get; set; } = null!;
    
    public decimal PurityMin { get; set; }   // e.g. 70%
    public decimal PurityMax { get; set; }   // e.g. 99%
    
    public string Grade { get; set; } = null!; // Industrial / Technical / Pharma

    public string UnitOfMeasure { get; set; } = "kg";

    public ICollection<SupplierRawMaterial> Suppliers { get; set; } = new List<SupplierRawMaterial>();
}