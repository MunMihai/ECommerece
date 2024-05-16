using Ecommerce.BL.Dto;
using Ecommerce.BL.Interfaces;
using Ecommerce.Core.Entities;

namespace Ecommerce.BL.Adapters;

public class ProductAdapter : IProductAdapter
{
    public Product ConvertToEntity(ProductDto productDto)
    {
        return new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Category = productDto.Category,
            Inventory = productDto.Inventory,
            UnitType = productDto.UnitType,
            ImageUrl = productDto.ImageUrl
        };
    }

    public ProductDto ConvertToDto(Product product)
    {
        return new ProductDto
        {
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            Inventory = product.Inventory,
            UnitType = product.UnitType,
            ImageUrl = product.ImageUrl
        };
    }

    public void UpdateEntityFromDto(ProductDto productDto, Product product)
    {
        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.Category = productDto.Category;
        product.Inventory = productDto.Inventory;
        product.UnitType = productDto.UnitType;
        product.ImageUrl = productDto.ImageUrl;
    }
}
