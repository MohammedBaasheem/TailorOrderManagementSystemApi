using System.ComponentModel.DataAnnotations;

namespace Tailor_Order_Management_System.Models.DTOs.Incoming
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string? Email { get; set; }
        [Required, StringLength(100)]
        public string? Password { get; set; }

        [Required, StringLength(100)]
        public string? Role { get; set; }
    }
}
