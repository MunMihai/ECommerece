using Ecommerce.BL.Dto;

namespace Ecommerce.BL.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task AddProductAsync(ProductDto product);
        Task UpdateProductAsync(Guid id, ProductDto product);
        Task DeleteProductAsync(Guid id);
    }
}
//facade pattern