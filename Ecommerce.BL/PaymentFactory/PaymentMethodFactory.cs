using Ecommerce.Core.Entities;

namespace Ecommerce.Core.PaymentFactory;

public class PaymentMethodFactory : IPaymentMethodFactory
{
    public IPaymentDetails? AddPaymentDetails(string method)
    {
        switch (method.ToLower())
        {
            case "cash":
                return new CashPaymentDetails();
            case "card":
                return new CardPaymentDetails();
            default:
                throw new ArgumentException("Invalid payment method.");
        }
    }
}