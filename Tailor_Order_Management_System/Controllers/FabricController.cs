using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Services.Interfaces;

namespace Tailor_Order_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricController : ControllerBase
    {
        private readonly IFabricService _fabricService;
        public FabricController(IFabricService fabricService)
        {
             _fabricService = fabricService;
        }
        [HttpGet("AllFabrics")]
        public async Task<IActionResult> GetAllFabricsAsync()
        {
            var result = await _fabricService.GetAllFabricsAsync();
            return Ok(result);
        }
        [HttpPost("AddFabric")]
        public async Task<IActionResult> AddFabricAsync([FromBody] AddFabricDTO model)
        {
            var result = await _fabricService.AddFabricAsync(model);
            return Ok(result);
        }
        [HttpPut("FabricId")]
        public async Task<IActionResult> UpdateFabricAsync(int FabricId, [FromBody] AddFabricDTO model)
        {
            var result = await _fabricService.UpdataFabricAsync(FabricId, model);
            return Ok(result);
        }

        [HttpGet("FabricId")]
        public async Task<IActionResult> GetFabricAsync(int FabricId)
        {
            var result = await _fabricService.GetFabricAsync(FabricId);
            return Ok(result);
        }
        [HttpDelete("FabricId")]
        public async Task<IActionResult> DeleteFabricAsync(int FabricId)
        {
            var result = await _fabricService.DeleteFabricAsync(FabricId);  
            return Ok(result);
        }
    }
}
