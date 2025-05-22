using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public required string Name { get; set; }

        public Customer Customer { get; set; } = null!;

        public ICollection<Product> Products { get; } = new List<Product>();
        public List<OrderRegel> OrderRegels { get; set; } = new List<OrderRegel>();
    }
}
