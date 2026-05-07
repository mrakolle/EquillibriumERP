using EquillibriumERP.Application.Services.Products;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Data;
using EquillibriumERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Infrastructure.Services.Products;

public class ProductsService : IProductsService
{
    private readonly TenantDbContextFactory _factory;

    public ProductsService(TenantDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<List<Product>> GetAll()
    {
        await using var db = _factory.Create();
        return await db.Products.ToListAsync();
    }
    public async Task<Product> Create(CreateProductRequest request)
    {
       // _trace.Step("ProductsService.Create START");
        await using var db = _factory.Create();
        //_trace.Step($"Tenant Schema = {db.Schema}");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            SKU = request.Sku,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };

        db.Products.Add(product);
        //_trace.Step("Before SaveChanges");
        await db.SaveChangesAsync();
        //_trace.Step("After SaveChanges");
        return product;
    }
}