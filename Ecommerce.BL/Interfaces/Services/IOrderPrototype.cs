using Ecommerce.Core.Entities;

namespace Ecommerce.BL.Interfaces.Services
{
    public interface IOrderPrototype
    {
        Order Clone(Order order);
    }
}
