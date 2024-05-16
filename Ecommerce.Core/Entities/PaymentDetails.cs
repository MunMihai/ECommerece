using Ecommerce.Core.Enums;

namespace Ecommerce.Core.Entities
{
    public interface IPaymentDetails
    {
        public Guid Id { get; set; } // Adding Id as the primary key

        public Guid UserId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
    public class CardPaymentDetails : IPaymentDetails
    {
        public Guid Id { get; set; } // Adding Id as the primary key

        public Guid UserId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
    }
    public class CashPaymentDetails : IPaymentDetails
    {
        public Guid Id { get; set; } // Adding Id as the primary key

        public Guid UserId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
