using AutoMapper;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Models.EntityClasses;

namespace Tailor_Order_Management_System.Helpres.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddOrderDTO, Order>()
                .ForMember(dest => dest.OrderCode, opt => opt.MapFrom(src => $"ORD{Guid.NewGuid().ToString().Substring(0, 6)}"))
                .ForMember(dest=>dest.status,opt=>opt.MapFrom(src=>"جاري العمل")).
                ForMember(dest=>dest.CreatedAt,opt=>opt.Ignore());

            CreateMap<Order, OrderDTO>().
                ForMember(dest => dest.ColorName, opt => opt.MapFrom(o => o.FabricColor.Color.Name)).
                ForMember(dest => dest.FabricName, opt => opt.MapFrom(f => f.FabricColor.Fabric.Name)).
                ForMember(dest => dest.Message, opt => opt.Ignore());

        }
        
    }
}
