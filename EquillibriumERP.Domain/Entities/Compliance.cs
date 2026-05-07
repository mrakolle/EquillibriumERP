using System;
using System.Collections.Generic;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class Compliance : BaseEntity
{
    // 1. Identification
    public Guid Id { get; set; }

    // 2. Compliance Details
    public string RegulationName { get; set; } = string.Empty;
    public string RegulatoryBody { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    //public bool ComplianceStatus { get; set; } = ComplianceStatus.NonCompliant;

    // 3. Documentation
    public string? DocumentUrl { get; set; }
    public string? Notes { get; set; }

    // 4. Associations
    public Guid? VendorId { get; set; }
    public Guid? ProductId { get; set; }
}
