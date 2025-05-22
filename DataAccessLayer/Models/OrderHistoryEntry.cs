using System;
using DataAccessLayer.Models;

public class OrderHistoryEntry
{
    public required string Voornaam { get; set; }
    public required string Achternaam { get; set; }
    public required string Adres { get; set; }
    public required string Woonplaats { get; set; }
    public required string Email { get; set; }
    public required string Telefoon { get; set; }
    public required string Betaalmethode { get; set; }

    public List<Shoppingcart> BesteldeItems { get; set; } = new();
    public DateTime Tijdstip { get; set; } = DateTime.Now;
}
