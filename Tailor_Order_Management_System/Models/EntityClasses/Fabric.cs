namespace Tailor_Order_Management_System.Models.EntityClasses
{
    public class Fabric
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal quantity { get; set; }


       


        public List<FabricColor> FabricColors { get; set; }
        




    }
}
