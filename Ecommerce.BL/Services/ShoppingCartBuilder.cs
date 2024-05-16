using Ecommerce.Core.Entities;

public class ShoppingCartBuilder : ICartBuilder
{
    private Cart _cart;

    public void Reset()
    {
        _cart = new Cart();
    }

    public void SetUserId(Guid userId)
    {
        _cart.UserId = userId;
    }

    public void SetCart(Cart cart)
    {
        _cart = cart;
    }

    public void AddProduct(Product product)
    {
        if (_cart.Products == null)
            _cart.Products = new List<Product>();

        _cart.Products.Add(product);
    }

    public decimal CalculatePrice(decimal currentTotalPrice)
    {

        foreach (var product in _cart.Products)
        {
            currentTotalPrice += product.Price;
        }
        return currentTotalPrice;
    }

    public void SetTotalPrice(decimal totalPrice)
    {
        _cart.TotalPrice = totalPrice;
    }

    public Cart GetCart()
    {
        return _cart;
    }
}
