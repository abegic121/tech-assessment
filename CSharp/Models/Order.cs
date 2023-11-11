using System;

namespace CSharp.Models
{
    public class Order
    {
        public int Id { get; set; } = 0;

        public int CustomerId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
