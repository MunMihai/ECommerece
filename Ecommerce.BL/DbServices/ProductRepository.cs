using Ecommerce.BL.DbServices;
using Ecommerce.BL.Dto;
using Ecommerce.BL.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IProductAdapter _adapter;

    public ProductRepository(AppDbContext dbContext, IProductAdapter adapter)
    {
        _dbContext = dbContext;
        _adapter = adapter;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _dbContext.Products.ToListAsync();
        var productsDto = new List<ProductDto>();

        foreach (var p in products)
        {
            var dto = _adapter.ConvertToDto(p);
            productsDto.Add(dto);
        }

        return productsDto;
    }


    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        return _adapter.ConvertToDto(product);
    }

    public async Task AddProductAsync(ProductDto product)
    {
        var entity = _adapter.ConvertToEntity(product);
        await _dbContext.Products.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Guid id, ProductDto product)
    {
        var entity = await _dbContext.Products.FindAsync(id);
        if (entity == null)
        {
            // Handle entity not found
            return;
        }

        _adapter.UpdateEntityFromDto(product, entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
