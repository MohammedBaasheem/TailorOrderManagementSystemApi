using Microsoft.AspNetCore.Mvc;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;

namespace Tailor_Order_Management_System.Services.Interfaces
{
    public interface IFabricService
    {
        Task<FabricDTO> AddFabricAsync(AddFabricDTO addFabricDTO);
        Task<FabricDTO> UpdataFabricAsync(int FabricId, AddFabricDTO addFabricDTO);
        
         Task<List<FabricDTO>> GetAllFabricsAsync();
        Task<FabricDTO> GetFabricAsync(int FabricId);
        Task<string> DeleteFabricAsync(int FabricId);
    }
}
