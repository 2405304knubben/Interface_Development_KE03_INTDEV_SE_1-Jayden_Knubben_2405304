using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public required string Name { get; set; } 

        public required string Password { get; set; }

        public string Address { get; set; } = string.Empty;

        public bool Active { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}