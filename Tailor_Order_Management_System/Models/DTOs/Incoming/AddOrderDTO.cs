using System.ComponentModel.DataAnnotations;
using Tailor_Order_Management_System.Models.EntityClasses;

namespace Tailor_Order_Management_System.Models.DTOs.Incoming
{
    public class AddOrderDTO
    {

        [Required, StringLength(100)]
        public string? CustomerName { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public int FabricColorId { get; set; }
 
    }
}
