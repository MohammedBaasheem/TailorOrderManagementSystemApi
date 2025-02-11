using System.ComponentModel.DataAnnotations;

namespace Tailor_Order_Management_System.Models.DTOs.Incoming
{
    public class TokenRequestModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
