//
// New Architecture
//
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Infrastructure.MultiTenancy;
using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Infrastructure.Persistence.Seed.Registry;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Api.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantsController : ControllerBase
{
    private readonly TenantProvisioningService _tenantService;
    private readonly TenantDbContextFactory _factory;
    private readonly MasterDbContext _masterDb;
    private readonly SeederRegistry _seederRegistry;

    public TenantsController(
        TenantProvisioningService tenantService,
        TenantDbContextFactory factory,
        MasterDbContext masterDb,
        SeederRegistry seederRegistry)
    {
        _tenantService = tenantService;
        _factory = factory;
        _masterDb = masterDb;
        _seederRegistry = seederRegistry;
    }

    // =====================================================
    // CREATE TENANT
    // =====================================================
    [HttpPost("create")]
    public async Task<ActionResult<Tenant>> CreateTenant([FromBody] CreateTenantRequest request)
    {
        var tenant = await _tenantService.CreateTenantAsync(request.Name);
        return Ok(tenant);
    }

    // =====================================================
    // SEED FULL TENANT (NEW ARCHITECTURE)
    // =====================================================
    [HttpPost("seed/{tenantId:guid}")]
    public async Task<IActionResult> SeedTenant(Guid tenantId)
    {
        var tenant = await _masterDb.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tenantId && t.IsActive);

        if (tenant == null)
            return BadRequest("Tenant not found or inactive");

        await using var context = _factory.Create(tenant.Schema);

        await using var transaction =
            await context.Database.BeginTransactionAsync();

        try
        {
            // 🚀 NEW: single orchestrated call instead of manual ordering
            await _seederRegistry.RunAllAsync(context);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return Ok(new
        {
            tenantId,
            schema = tenant.Schema,
            message = "Tenant seeding completed successfully"
        });
    }

    // =====================================================
    // OPTIONAL: SEED BY GROUP (NEW FEATURE)
    // =====================================================
    [HttpPost("seed/{tenantId:guid}/group/{group}")]
    public async Task<IActionResult> SeedTenantGroup(Guid tenantId, string group)
    {
        var tenant = await _masterDb.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tenantId && t.IsActive);

        if (tenant == null)
            return BadRequest("Tenant not found or inactive");

        await using var context = _factory.Create(tenant.Schema);

        await using var transaction =
            await context.Database.BeginTransactionAsync();

        try
        {
            await _seederRegistry.RunGroupAsync(group, context);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return Ok(new
        {
            tenantId,
            group,
            schema = tenant.Schema,
            message = $"Tenant group '{group}' seeding completed"
        });
    }
}

public class CreateTenantRequest
{
    public required string Name { get; set; }
}





//
//Old Architecture
//
/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Infrastructure.MultiTenancy;
using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Infrastructure.Persistence.Seed.Manufacturing;
using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Api.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantsController : ControllerBase
{
    private readonly TenantProvisioningService _tenantService;
    private readonly TenantDbContextFactory _factory;
    private readonly MasterDbContext _masterDb;

    public TenantsController(
        TenantProvisioningService tenantService,
        TenantDbContextFactory factory,
        MasterDbContext masterDb)
    {
        _tenantService = tenantService;
        _factory = factory;
        _masterDb = masterDb;
    }

    // =====================================================
    // CREATE TENANT (from TenantsController)
    // =====================================================
    [HttpPost("create")]
    public async Task<ActionResult<Tenant>> CreateTenant([FromBody] CreateTenantRequest request)
    {
        var tenant = await _tenantService.CreateTenantAsync(request.Name);
        return Ok(tenant);
    }

    // =====================================================
    // SEED TENANT (from TenantSeedController)
    // =====================================================
    [HttpPost("seed/{tenantId:guid}")]
    public async Task<IActionResult> SeedTenant(Guid tenantId)
    {
        // 🔥 Get real tenant from Master DB (single source of truth)
        var tenant = await _masterDb.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tenantId && t.IsActive);

        if (tenant == null)
            return BadRequest("Tenant not found or inactive");

        var schema = tenant.Schema;

        // 🔥 Create tenant-specific DbContext
        await using var context = _factory.Create(schema);

        // 🔥 Seed order matters (UNCHANGED LOGIC)
        await using var transaction =
            await context.Database.BeginTransactionAsync();

        await SupplierSeeder.SeedAsync(context);
        await RawMaterialSeeder.SeedAsync(context);
        await ProductSeeder.SeedAsync(context);
        await BOMSeeder.SeedAsync(context);

        await transaction.CommitAsync();

        return Ok(new
        {
            tenantId,
            schema,
            message = "Tenant seeding completed successfully"
        });
    }
}

public class CreateTenantRequest
{
    public required string Name { get; set; }
}
*/


/*using Microsoft.AspNetCore.Mvc;
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
}*/