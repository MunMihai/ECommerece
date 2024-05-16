using Ecommerce.BL.Interfaces;
using MediatR;
namespace Ecommerce.BL.Commands;

public class AddProductToCartCommand : IRequest<Unit>
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
}

public class RemoveProductFromCartCommand : IRequest<Unit>
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
}


public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand, Unit>
{
    private readonly ICartService _cartService;

    public AddProductToCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<Unit> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
        await _cartService.AddProductToCart(request.CartId, request.ProductId);
        return Unit.Value;
    }

}

public class RemoveProductFromCartCommandHandler : IRequestHandler<RemoveProductFromCartCommand, Unit>
{
    private readonly ICartService _cartService;

    public RemoveProductFromCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<Unit> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
    {
        await _cartService.RemoveProductFromCart(request.CartId, request.ProductId);
        return Unit.Value;
    }
}

