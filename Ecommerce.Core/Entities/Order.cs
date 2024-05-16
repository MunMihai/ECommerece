using Ecommerce.Core.Enums;

namespace Ecommerce.Core.Entities
{
    public class Order
    {
        public Order()
        {
        }
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }
        public CardPaymentDetails? CardPaymentDetails { get; set; }
        public CashPaymentDetails? CashPaymentDetails { get; set; }
        //public IPaymentDetails? PaymentDetails { get; set; }


    }
}

