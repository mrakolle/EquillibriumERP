namespace EquillibriumERP.Infrastructure.MultiTenancy;

public interface ITenantProvider
{
    string? TenantId { get; }
    string? Schema { get; }

    void SetTenant(string tenantId, string schema);
}