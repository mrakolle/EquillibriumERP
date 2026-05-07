using EquillibriumERP.Domain.Entities;
namespace EquillibriumERP.Application.Services.Products;public interface IProductsService
{
    Task<List<Product>> GetAll();
    Task<Product> Create(CreateProductRequest request);
}