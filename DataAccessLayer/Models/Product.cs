using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Product
    {        
        public int Id { get; set; }

        public required string Name { get; set; }

        public required  string Description { get; set; }

        public required decimal Price { get; set; }
        public required string ImageUrl { get; set; }
        public required string ImageAuthor { get; set; }        

        public ICollection<Order> Orders { get; } = new List<Order>();

        public ICollection<Part> Parts { get; } = new List<Part>();
    }
}
