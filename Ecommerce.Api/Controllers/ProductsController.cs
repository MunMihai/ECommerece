using Ecommerce.BL.Dto;
using Ecommerce.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;

    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] ProductDto product)
    {
        await _productRepository.AddProductAsync(product);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] ProductDto product)
    {
        await _productRepository.UpdateProductAsync(id, product);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productRepository.DeleteProductAsync(id);
        return NoContent();
    }
}

