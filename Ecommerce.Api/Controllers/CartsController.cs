using Ecommerce.BL.Commands;
using Ecommerce.BL.Dto;
using Ecommerce.BL.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    private Guid _userId;
    private readonly IMediator _mediator;

    public CartsController(ICartService cartService, IMyProfileService myProfileService, IMediator mediator)
    {
        _cartService = cartService;
        _userId = myProfileService.GetUserId();
        _mediator = mediator;
    }
    [HttpPost("create-empty-cart")]
    public async Task<IActionResult> CreateEmptyCart()
    {
        await _cartService.CreateEmptyCartAsync(_userId);

        return Ok();

    }
    [HttpGet("my-current-cart")]
    public async Task<ActionResult<CartDto>> GetCart()
    {
        var cart = await _cartService.GetCartByUserId(_userId);
        if (cart == null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    [HttpPost("products")]
    public async Task<ActionResult<CartDto>> AddProductToCart(Guid cartId, Guid productId)
    {
        var command = new AddProductToCartCommand { CartId = cartId, ProductId = productId };
        var cart = await _mediator.Send(command);

        return Ok(cart);
    }

    [HttpDelete("products/{productId}")]
    public async Task<ActionResult<CartDto>> RemoveProductFromCart(Guid cartId, Guid productId)
    {
        var command = new RemoveProductFromCartCommand { CartId = cartId, ProductId = productId };
        var cart = await _mediator.Send(command);

        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }
}

