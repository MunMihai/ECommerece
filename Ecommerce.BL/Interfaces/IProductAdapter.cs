using Ecommerce.BL.Dto;
using Ecommerce.Core.Entities;

namespace Ecommerce.BL.Interfaces;

public interface IProductAdapter
{
    Product ConvertToEntity(ProductDto productDto);
    ProductDto ConvertToDto(Product product);
    void UpdateEntityFromDto(ProductDto productDto, Product product);
}
