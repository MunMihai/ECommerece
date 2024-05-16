using Ecommerce.BL.Dto;

namespace Ecommerce.BL.Interfaces.Services
{
    public interface IBaseOrderService
    {
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<OrderDto> CloneOrderAsync(Guid orderId);
    }

    public interface IOrderService : IBaseOrderService
    {
        Task CheckoutOrderAsync(Guid userId, OrderDto orderDto);
        Task<IEnumerable<OrderDto>> GetAllOrdersByUserIdAsync(Guid userId);
    }

    public interface IOrderServiceProxy : IBaseOrderService
    {
        Task CheckoutOrderAsync(OrderDto orderDto);
        Task<IEnumerable<OrderDto>> GetAllOrdersByUserIdAsync();
    }
}
