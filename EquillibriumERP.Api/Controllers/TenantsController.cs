using Microsoft.AspNetCore.Mvc;
using EquillibriumERP.Infrastructure.MultiTenancy;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Api.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantsController : ControllerBase
{
    private readonly TenantProvisioningService _tenantService;

    public TenantsController(TenantProvisioningService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Tenant>> CreateTenant([FromBody] CreateTenantRequest request)
    {
        //_trace.Step("TenantController.Create START");
        var tenant = await _tenantService.CreateTenantAsync(request.Name);
       // _trace.Step("TenantController.Create END");
        return Ok(tenant);
    }
}

public class CreateTenantRequest
{
    public required string Name { get; set; }
}