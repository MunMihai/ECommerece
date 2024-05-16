using AutoMapper;
using Ecommerce.BL.DbServices;
using Ecommerce.BL.Dto;
using Ecommerce.BL.Interfaces;
using Ecommerce.BL.Interfaces.Services;
using Ecommerce.Core.Entities;
using Ecommerce.Core.PaymentFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderPrototype _orderPrototype;
        private readonly AppDbContext _appDbContext;

        private readonly IPaymentMethodFactory _paymentMethodFactory;

        public OrderService(IMapper mapper, IOrderPrototype orderPrototype, AppDbContext appDbContext, IPaymentMethodFactory paymentMethodFactory)
        {
            _mapper = mapper;
            _orderPrototype = orderPrototype;
            _appDbContext = appDbContext;
            _paymentMethodFactory = paymentMethodFactory;
        }

        public async Task CheckoutOrderAsync(Guid userId, OrderDto orderDto)
        {
            var order = new Order
            {
                UserId = userId,
                CartId = orderDto.CartId,
                Id = new Guid(),
                PhoneNumber = orderDto.PhoneNumber,
                Status = orderDto.Status,
                Adress = orderDto.Adress,
                PaymentMethod = orderDto.PaymentMethod,
                //PaymentDetails = _paymentMethodFactory.AddPaymentDetails(orderDto.PaymentMethod)
            };
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _appDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException($"Order with ID {orderId} not found.");
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CloneOrderAsync(Guid orderId)
        {
            var orderToClone = await _appDbContext.Orders.FindAsync(orderId);
            if (orderToClone == null)
            {
                throw new ArgumentException($"Order with ID {orderId} not found.");
            }

            var clonedOrder = _orderPrototype.Clone(orderToClone);
            _appDbContext.Orders.Add(clonedOrder);
            _appDbContext.SaveChanges();
            return _mapper.Map<OrderDto>(clonedOrder);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _appDbContext.Orders
                .Where(o => o.UserId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}

public class OrderServiceProxy : IOrderServiceProxy
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderServiceProxy> _logger;
    private readonly Guid _userId;

    public OrderServiceProxy(IOrderService orderService, IMyProfileService myProfileService, ILogger<OrderServiceProxy> logger)
    {
        _orderService = orderService;
        _logger = logger;
        _userId = myProfileService.GetUserId();
    }

    public async Task CheckoutOrderAsync(OrderDto orderDto)
    {
        _logger.LogInformation($"User {_userId} is checking out order1.");
        await _orderService.CheckoutOrderAsync(_userId, orderDto);
        _logger.LogInformation($"Order checked out successfully.");
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
    {
        _logger.LogInformation($"Getting order with ID {orderId}.");
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null)
        {
            _logger.LogWarning($"Order with ID {orderId} not found.");
        }
        _logger.LogInformation($"Got order with ID {orderId}.");
        return order;
    }

    public async Task<OrderDto> CloneOrderAsync(Guid orderId)
    {
        _logger.LogInformation($"User {_userId} is cloning order with ID {orderId}.");
        var clonedOrder = await _orderService.CloneOrderAsync(orderId);
        if (clonedOrder == null)
        {
            _logger.LogWarning($"Order with ID {orderId} not found.");
        }
        else
        {
            _logger.LogInformation($"User {_userId} successfully cloned order with ID {orderId}.");
        }
        return clonedOrder;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersByUserIdAsync()
    {
        _logger.LogInformation($"Getting all orders for user {_userId}.");
        var orders = await _orderService.GetAllOrdersByUserIdAsync(_userId);
        return orders;
    }
}

