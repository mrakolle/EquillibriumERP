using ManufacturingERP.Infrastructure.MultiTenancy;

namespace ManufacturingERP.Infrastructure.Data;

public class DesignTimeTenantProvider : ITenantProvider
{
    public string? TenantId => "design_time";
    public string? Schema => "design_time";

    public void SetTenant(string tenantId, string schema)
    {
        // no-op for design time
    }
}