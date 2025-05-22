using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Part
    {
        public int Id { get; set; }

        public required string Name { get; set; } 
        public required decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public required string ImageUrl { get; set; }
        public required string ImageAuthor { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
