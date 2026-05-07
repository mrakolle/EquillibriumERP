using System;

namespace EquillibriumERP.Infrastructure.MultiTenancy;

public class TenantProvider : ITenantProvider
{
    public string? TenantId { get; private set; }
    public string? Schema { get; private set; }

    public void SetTenant(string tenantId, string schema)
    {
        TenantId = tenantId;
        Schema = schema;
    }
}