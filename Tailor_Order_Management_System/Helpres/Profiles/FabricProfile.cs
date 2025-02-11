using AutoMapper;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Models.EntityClasses;

namespace Tailor_Order_Management_System.Helpres.Profiles
{
    public class FabricProfile : Profile
    {
        public FabricProfile()
        {
            CreateMap<Fabric,AddFabricDTO>().ForMember(dest=>dest.ColorsNames,op=>op.Ignore());


            //CreateMap<AddFabricDTO,Fabric>().ForMember(dest=>dest.Orders,op=>op.Ignore())
            //    .ForMember(dest=>dest.Orders,opt=>opt.Ignore()).
            //    ForMember(dest=>dest.FabricColors,opt=>opt.Ignore());


            //CreateMap<FabricDTO, Fabric>().ForMember(dest => dest.Orders, op => op.Ignore())
            //    .ForMember(dest => dest.FabricColors, opt => opt.Ignore());

            CreateMap<Fabric, FabricDTO>().ForMember(dest => dest.Message, op => op.Ignore()).
                ForMember(dset => dset.FabricColorsNmaes, op => op.MapFrom(S=>S.FabricColors.Select(s=>s.Color.Name)));
                
            
        }
    }
}
