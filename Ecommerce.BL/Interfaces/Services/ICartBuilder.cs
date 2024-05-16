using Ecommerce.Core.Entities;

public interface ICartBuilder
{
    void Reset();
    void SetUserId(Guid userId);
    void SetCart(Cart cart);
    void AddProduct(Product product);
    decimal CalculatePrice(decimal current);
    void SetTotalPrice(decimal totalPrice);
    Cart GetCart();
}
