using System.ComponentModel.DataAnnotations;

namespace Tailor_Order_Management_System.Models.DTOs.Incoming
{
    public class AddRoleModel
    {
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
