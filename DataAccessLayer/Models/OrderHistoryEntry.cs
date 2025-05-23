using System;
using DataAccessLayer.Models;

public class OrderHistoryEntry
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Address { get; set; }
    public required string Location { get; set; }
    public required string Email { get; set; }
    public required string Phonenumber { get; set; }
    public required string Paying_Method { get; set; }

    public List<Shoppingcart> OrderedItems { get; set; } = new();
    public DateTime Time { get; set; } = DateTime.Now;
}
