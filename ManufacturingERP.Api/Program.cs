using ManufacturingERP.Infrastructure.DependencyInjection;
using ManufacturingERP.Infrastructure.MultiTenancy;
using ManufacturingERP.Infrastructure.Services.Products;
using ManufacturingERP.Application.Services.Products;
using ManufacturingERP.Application.Common.Interfaces;
using ManufacturingERP.Infrastructure.Persistence;
using ManufacturingERP.Api.Tracing;

using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// -------------------------------------------------
// SERVICES (DI)
// -------------------------------------------------

builder.Services.AddControllers();

// 🔥 Multi-tenancy core
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<TenantDbContextFactory>();

// 🔥 Application services
builder.Services.AddScoped<IProductsService, ProductsService>();

// 🔥 Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 Infrastructure (PublicDbContext lives here)
builder.Services.AddInfrastructure(configuration);

// ❌ DO NOT REGISTER TenantDbContext
// builder.Services.AddDbContext<TenantDbContext>(); <-- NEVER again

builder.Services.AddScoped<IRequestTracer, RequestTracer>();
builder.Services.AddScoped<RequestExecutionContext>();

// -------------------------------------------------
// BUILD
// -------------------------------------------------

var app = builder.Build();

// =================================================
// FIXED SEEDING (SAFE + IDPOTENT + NO DB LEAKAGE)
// =================================================
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

    // ONLY ONE ENTRY POINT - Seeder handles ALL logic internally
    await seeder.SeedAsync();
}

// -------------------------------------------------
// PIPELINE
// -------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔥 CRITICAL: tenant resolution BEFORE controllers
app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();