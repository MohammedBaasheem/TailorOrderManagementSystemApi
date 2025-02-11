using Sieve.Models;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Models.EntityClasses;

namespace Tailor_Order_Management_System.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrderAsync(AddOrderDTO addOrderDTO);
        Task<List<OrderDTO>> GetAllOrdersAsync(SieveModel sieveModel);
        Task<OrderDTO> GetOrderAsync(int OrderId);
        Task<OrderDTO> UpdataeOrderStatusAsync(int orderId,string Status);
        Task<string> DeleteOrderAsync(int OrderId);
    }
}
