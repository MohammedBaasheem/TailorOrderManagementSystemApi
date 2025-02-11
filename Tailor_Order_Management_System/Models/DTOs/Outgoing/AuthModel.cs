using System.Text.Json.Serialization;

namespace Tailor_Order_Management_System.Models.DTOs.Outgoing
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuthentcated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        //public DateTime ExpirasOn { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirasOn { get; set; }
    }
}
