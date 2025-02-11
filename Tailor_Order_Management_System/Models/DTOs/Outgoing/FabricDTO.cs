using Tailor_Order_Management_System.Models.EntityClasses;

namespace Tailor_Order_Management_System.Models.DTOs.Outgoing
{
    public class FabricDTO
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public decimal quantity { get; set; }


       // public List<Order> OrdersNames { get; set; }


        public List<string> FabricColorsNmaes { get; set; }
    }
}
