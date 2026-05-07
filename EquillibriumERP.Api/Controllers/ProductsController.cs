using Microsoft.AspNetCore.Mvc;
using EquillibriumERP.Application.Services.Products;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;

    public ProductsController(IProductsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAll();
        return Ok(result);
    }
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        //_trace.Step("ProductsController.Create START");
        var product = await _service.Create(request);
       // _trace.Step("ProductsController.Create END");
        return Ok(product);
    }
}