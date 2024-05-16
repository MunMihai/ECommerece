using AutoMapper;
using Ecommerce.BL.DbServices;
using Ecommerce.BL.Dto;
using Ecommerce.BL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.BL.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICartBuilder _cartBuilder;

        public CartService(AppDbContext dbContext, IMapper mapper, ICartBuilder cartBuilder)
        {
            _appDbContext = dbContext;
            _mapper = mapper;
            _cartBuilder = cartBuilder;
        }

        public async Task CreateEmptyCartAsync(Guid userId)
        {
            _cartBuilder.Reset();
            _cartBuilder.SetUserId(userId);
            var cart = _cartBuilder.GetCart();

            _appDbContext.Carts.Add(cart);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<CartDto> AddProductToCart(Guid cartId, Guid productId)
        {
            var cart = await _appDbContext.Carts.FindAsync(cartId);
            if (cart == null)
            {
                throw new ArgumentException($"Cart with ID {cartId} not found.");
            }

            var product = await _appDbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {productId} not found.");
            }

            _cartBuilder.SetCart(cart);
            _cartBuilder.AddProduct(product);

            cart.TotalPrice = _cartBuilder.CalculatePrice(cart.TotalPrice);

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByCartId(Guid cartId)
        {
            var cart = await _appDbContext.Carts.FindAsync(cartId);
            if (cart == null)
            {
                throw new ArgumentException($"Cart with ID {cartId} not found.");
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(Guid userId)
        {
            var cart = await _appDbContext.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ArgumentException($"Cart for user with ID {userId} not found.");
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> RemoveProductFromCart(Guid cartId, Guid productId)
        {
            var cart = await _appDbContext.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
            {
                throw new ArgumentException($"Cart with ID {cartId} not found.");
            }

            var productToRemove = cart.Products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                cart.Products.Remove(productToRemove);
                cart.TotalPrice -= productToRemove.Price;

                await _appDbContext.SaveChangesAsync();
            }

            return _mapper.Map<CartDto>(cart);
        }
    }
}
