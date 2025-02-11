using Microsoft.Win32;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;

namespace Tailor_Order_Management_System.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registermodel);
        Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel);
        Task<string> AddRloeAsync(AddRoleModel addRoleModel);
        Task<AuthModel> RefreshTokenAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string refreshToken);
    }
}
