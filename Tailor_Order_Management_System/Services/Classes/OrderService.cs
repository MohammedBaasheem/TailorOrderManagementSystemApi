using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Runtime.Intrinsics.X86;
using Tailor_Order_Management_System.Exceptions;
using Tailor_Order_Management_System.Models.DbContext;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Models.EntityClasses;
using Tailor_Order_Management_System.Services.Interfaces;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace Tailor_Order_Management_System.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _sieveProcessor;
        public OrderService(IMapper mapper, ApplicationDbContext context, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _context = context;
            _sieveProcessor = sieveProcessor;
        }
        public async Task<OrderDTO> AddOrderAsync(AddOrderDTO addOrderDTO)
        {
            var fabriccolor = await _context.FabricColors.Include(c => c.Fabric).Include(f => f.Color).FirstOrDefaultAsync(fc => fc.Id == addOrderDTO.FabricColorId);

            if (fabriccolor is not null)
            {
                
                var order = _mapper.Map<Order>(addOrderDTO);
                order.status = "جاري التنفيذ";
                await _context.AddAsync(order);
                await _context.SaveChangesAsync();
                var dto = _mapper.Map<OrderDTO>(order);
                dto.Message = "The Order Added Seccssfully.  ";
                return dto;

            }
            else
            {
                throw new NotFoundException("The Fabric or FabricColor are Not Found ");
            }
        }

        public async Task<OrderDTO> GetOrderAsync(int OrderId)
        {

            var order = await _context.Orders.Include(f => f.FabricColor.Fabric).Include(c => c.FabricColor.Color).FirstOrDefaultAsync(o => o.ID == OrderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Thy Order Not Found"); 
            }

            var dto = _mapper.Map<OrderDTO>(order);
            return dto;
        }
        public async Task<List<OrderDTO>> GetAllOrdersAsync(SieveModel sieveModel)
        {
            // استعلام الطلبات مع التحميل المسبق للألوان والأقمشة
            var ordersQuery = _context.Orders
                .Include(f => f.FabricColor.Fabric)
                .Include(c => c.FabricColor.Color)
                .AsQueryable();
            if(ordersQuery.Count() > 0)
            {
                // تطبيق Sieve للفلترة والبحث والترتيب
                var filteredOrders = _sieveProcessor.Apply(sieveModel, ordersQuery);

                // استعلام البيانات بعد تطبيق الفلترة
                var orders = await filteredOrders.ToListAsync();

                // تحويل الطلبات إلى OrderDTO
                var dto = _mapper.Map<List<OrderDTO>>(orders);
                return dto;
            }
            else
            {
                throw new NotFoundException("The Fabric or FabricColor are Not Found ");
            }

            
        }

        public async Task<OrderDTO> UpdataeOrderStatusAsync(int orderId, string Status)
        {
            if(Status!="جاهز"&& Status!="جاري العمل عليه")
            {
                throw new BadRequestException($"{Status} Are Not Correct Status");
            }
            var order = await _context.Orders.Include(f => f.FabricColor.Fabric).Include(c => c.FabricColor.Color).FirstOrDefaultAsync(o => o.ID == orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Thy Order Not Found");
            }
            else
            {
                order.status = Status;
                await _context.SaveChangesAsync();
                var dto = _mapper.Map<OrderDTO>(order);
                dto.Message = "The OrderStatus Updated Seccssfully.  ";
                return dto;

            }

        }
        public async Task<string> DeleteOrderAsync(int OrderId)
        {
            var order = await _context.Orders.Include(f => f.FabricColor.Fabric).Include(c => c.FabricColor.Color).FirstOrDefaultAsync(o => o.ID == OrderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Thy Order Not Found");
            }
            else
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return "This Order is remove seccssfully.";
            }
            
           
        }
    }
}
