using System;
using EquillibriumERP.Domain.Common;

namespace EquillibriumERP.Domain.Entities;

public class QCApproval : BaseEntity
{
    public Guid BatchId { get; set; }

    public bool Approved { get; set; }

    public string ApprovedBy { get; set; } = string.Empty;

    public DateTime ApprovedAt { get; set; }
}