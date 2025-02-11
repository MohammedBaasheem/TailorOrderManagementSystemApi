using Sieve.Attributes;

namespace Tailor_Order_Management_System.Models.EntityClasses
{
    public class Order
    {
        public int ID { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string? CustomerName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]

        public int price { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]

        public int quantity { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]

        public string status { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string OrderCode { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]

        public int FabricColorId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Sieve(CanFilter = true, CanSort = true)]
        public FabricColor FabricColor { get; set; }
    }
}
