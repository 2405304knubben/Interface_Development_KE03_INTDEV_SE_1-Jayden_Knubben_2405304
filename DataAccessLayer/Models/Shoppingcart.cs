using System;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Models;


public class Shoppingcart
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsPart { get; set; }
}