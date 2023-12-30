using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Enitites.Order_Aggregate;
using Talabat.Core.Services;


namespace Talabat.API.Controllers
{

    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost] // Post : /api/orde
        [ProducesResponseType(typeof(OrderToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orderAddress = _mapper.Map<AddressDTO, OrderAddress>(orderDTO.ShippiingAddress);
            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDTO.BasketId, orderDTO.DeliveryMethodId, orderAddress);

            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDTO>(order));

        }
        [HttpGet] // GET : /api/orders 
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUsersAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(orders));
        }

        [HttpGet("{id}")] // /api/orders/id
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrdersByIdForUsersAsync(id, buyerEmail);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDTO>(order));
        }

        [HttpGet("deliverymethod")]
        public async Task<ActionResult<IReadOnlySet<DeliveryMethod>>> GetDeliverMethod()
        {
            var deliveryMethod = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethod);
        }




    }
}
