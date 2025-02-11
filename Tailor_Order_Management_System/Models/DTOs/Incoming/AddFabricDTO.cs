namespace Tailor_Order_Management_System.Models.DTOs.Incoming
{
    public class AddFabricDTO
    {
      
        public string Name { get; set; }
        public decimal quantity { get; set; }
        public List<string> ColorsNames { get; set; }

    }
}
