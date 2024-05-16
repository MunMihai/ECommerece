using Ecommerce.BL.Interfaces.Services;
using Ecommerce.Core.Entities;

namespace Ecommerce.BL.Services
{
    public class OrderPrototype : IOrderPrototype
    {
        public Order Clone(Order order)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                CartId = order.CartId,
                UserId = order.UserId,
                Status = order.Status,
                Adress = order.Adress,
                PhoneNumber = order.PhoneNumber,
                PaymentMethod = "cash"
            };
        }
    }
}
