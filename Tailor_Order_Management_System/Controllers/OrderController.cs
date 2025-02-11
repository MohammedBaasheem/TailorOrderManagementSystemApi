using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Tailor_Order_Management_System.Exceptions;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Models.EntityClasses;
using Tailor_Order_Management_System.Services.Classes;
using Tailor_Order_Management_System.Services.Interfaces;

namespace Tailor_Order_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrderAsync([FromBody] AddOrderDTO model)
        {
            var result = await _orderService.AddOrderAsync(model);
            return Ok(result);
        }
        [HttpGet("AllOreders")]
        public async Task<IActionResult> GetAllOredersAsync([FromQuery] SieveModel sieveModel)
        {
            var result = await _orderService.GetAllOrdersAsync(sieveModel);
            return Ok(result);
        }

        [HttpGet("OrederId")]
        public async Task<IActionResult> GetOrederAsync(int OrederId)
        {
            var result = await _orderService.GetOrderAsync(OrederId);
            return Ok(result);
        }
        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatusAsync(int orderId, [FromBody] string status)
        {
            var result = await _orderService.UpdataeOrderStatusAsync(orderId, status);
            return Ok(result);
        }
        [HttpDelete("{OrderId}")]
        public async Task<IActionResult> DeleteOrderAsync(int OrderId)
        {
            var result = await _orderService.DeleteOrderAsync(OrderId);
            return Ok(result);
        }

    }
}

