namespace Tailor_Order_Management_System.Models.EntityClasses
{
    public class FabricColor
    {
        public int Id { get; set; }
        public int FabricId { get; set; }
        public Fabric Fabric { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public List<Order> Orders { get; set; }
    }
}
