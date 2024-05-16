namespace Ecommerce.Core.Entities;

public class Cart
{
    public Cart()
    {
    }
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<Product> Products { get; set; }
    public decimal TotalPrice { get; set; }
}

