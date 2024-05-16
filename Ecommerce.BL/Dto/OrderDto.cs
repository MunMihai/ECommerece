using Ecommerce.Core.Enums;

namespace Ecommerce.BL.Dto
{
    public class OrderDto
    {
        public Guid CartId { get; set; }
        public Status Status { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }
    }
}

