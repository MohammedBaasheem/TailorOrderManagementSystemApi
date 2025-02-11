using Sieve.Attributes;
using Tailor_Order_Management_System.Models.EntityClasses;

namespace Tailor_Order_Management_System.Models.DTOs.Outgoing
{
    public class OrderDTO
    {

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
        public string ColorName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string FabricName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime CreatedAt { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Message { get; set; }
        
    }
}
