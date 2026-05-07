using Serilog.Core;
using Serilog.Events;
using Microsoft.AspNetCore.Http;

namespace EquillibriumERP.Infrastructure.MultiTenancy;

public class TenantLogEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantLogEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var context = _httpContextAccessor.HttpContext;

        if (context == null) return;

        var tenantHeader = context.Request.Headers["X-Tenant-Id"].ToString();

        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty("TenantId", tenantHeader));

        var tenantProvider = context.RequestServices.GetService(typeof(ITenantProvider)) as ITenantProvider;

        if (tenantProvider != null)
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("Schema", tenantProvider.Schema));
        }
    }
}