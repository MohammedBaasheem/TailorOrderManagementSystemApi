using System.ComponentModel.DataAnnotations;

namespace Tailor_Order_Management_System.Models.EntityClasses
{
    public class Color
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<FabricColor> FabricColors { get; set; }
    }
}
