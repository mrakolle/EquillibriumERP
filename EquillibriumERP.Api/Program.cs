using EquillibriumERP.Infrastructure.DependencyInjection;
using EquillibriumERP.Infrastructure.MultiTenancy;
using EquillibriumERP.Infrastructure.Services.Products;
using EquillibriumERP.Application.Services.Products;
using EquillibriumERP.Application.Common.Interfaces;
using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Infrastructure.Persistence.Seed.Registry;
using EquillibriumERP.Infrastructure.Persistence.Seed.Abstractions;
using EquillibriumERP.Infrastructure.Persistence.Seed.Manufacturing;
using EquillibriumERP.Api.Tracing;

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

// 🔥 Infrastructure
builder.Services.AddInfrastructure(configuration);

// ❌ NEVER register TenantDbContext directly
// builder.Services.AddDbContext<TenantDbContext>();

// -------------------------------------------------
// SEEDER REGISTRY
// -------------------------------------------------

builder.Services.AddScoped<SeederRegistry>();

// Manufacturing seeders
builder.Services.AddScoped<ISeeder, SupplierSeeder>();
builder.Services.AddScoped<ISeeder, RawMaterialSeeder>();
builder.Services.AddScoped<ISeeder, ProductSeeder>();
builder.Services.AddScoped<ISeeder, BomSeeder>();
builder.Services.AddScoped<ISeeder, SupplierRawMaterialSeeder>();

// -------------------------------------------------
// TRACING
// -------------------------------------------------

builder.Services.AddScoped<IRequestTracer, RequestTracer>();
builder.Services.AddScoped<RequestExecutionContext>();

// -------------------------------------------------
// BUILD
// -------------------------------------------------

var app = builder.Build();

// -------------------------------------------------
// PIPELINE
// -------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔥 CRITICAL:
// Tenant resolution MUST happen BEFORE controllers
app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();