using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class OrderRegel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}