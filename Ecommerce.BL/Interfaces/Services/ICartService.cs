using Ecommerce.BL.Dto;

namespace Ecommerce.BL.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserId(Guid userId);
        Task<CartDto> GetCartByCartId(Guid cartId);

        Task<CartDto> AddProductToCart(Guid cartId, Guid productId);
        Task<CartDto> RemoveProductFromCart(Guid cartId, Guid productId);

        Task CreateEmptyCartAsync(Guid userId);

    }
}
