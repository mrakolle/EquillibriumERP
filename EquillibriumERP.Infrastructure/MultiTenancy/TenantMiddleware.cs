using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Infrastructure.Persistence;

namespace EquillibriumERP.Infrastructure.MultiTenancy;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
    HttpContext context,
    ITenantProvider tenantProvider,
    MasterDbContext publicDb)
    {
        var tenantHeader = context.Request.Headers["X-Tenant-Id"].ToString();
        var path = context.Request.Path.Value?.ToLower();

        if (path != null && path.StartsWith("/api/tenants"))
        {
            if (!string.IsNullOrWhiteSpace(tenantHeader) &&
                Guid.TryParse(tenantHeader, out var parsedTenantId))
            {
                var existingTenant = await publicDb.Tenants
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == parsedTenantId && t.IsActive);

                if (existingTenant != null)
                {
                    tenantProvider.SetTenant(parsedTenantId.ToString(), existingTenant.Schema);
                }
            }

            await _next(context);
            return;
        }
        if (string.IsNullOrWhiteSpace(tenantHeader))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Missing tenant header");
            return;
        }

        if (!Guid.TryParse(tenantHeader, out var tenantId))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid tenant header");
            return;
        }

        // 🔥 VALIDATE AGAINST PUBLIC DB
        var tenant = await publicDb.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tenantId && t.IsActive);

        if (tenant == null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Tenant not found or inactive");
            return;
        }

        tenantProvider.SetTenant(tenantId.ToString(), tenant.Schema);

        await _next(context);
    }
}