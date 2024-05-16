using Ecommerce.Core.Entities;

namespace Ecommerce.Core.PaymentFactory;

public interface IPaymentMethodFactory
{
    IPaymentDetails? AddPaymentDetails(string method);
}
