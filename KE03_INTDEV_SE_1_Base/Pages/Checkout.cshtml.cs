using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models; // Voor Shoppingcart
using System.Collections.Generic;
using System.Linq;
using System;

public class CheckoutModel : PageModel
{
    [BindProperty]
    public Bestelformulier Form { get; set; } = new()
    {
        Naam = string.Empty,
        Achternaam = string.Empty,
        Adres = string.Empty,
        Woonplaats = string.Empty,
        Email = string.Empty,
        Telefoonnummer = string.Empty,
        Betaalmethode = string.Empty
    };

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Winkelwagen ophalen
        var winkelwagen = Request.Cookies.GetObjectFromJson<List<Shoppingcart>>("Winkelwagen") ?? new();

        if (!winkelwagen.Any())
        {
            TempData["SuccessMessage"] = "Geen producten in de winkelwagen.";
            return RedirectToPage("/Index");
        }

        // Nieuwe bestelling aanmaken
        var bestelling = new OrderHistoryEntry
        {
            Voornaam = Form.Naam,
            Achternaam = Form.Achternaam,
            Adres = Form.Adres,
            Woonplaats = Form.Woonplaats,
            Email = Form.Email,
            Telefoon = Form.Telefoonnummer,
            Betaalmethode = Form.Betaalmethode,
            BesteldeItems = winkelwagen.Select(p => new Shoppingcart
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList(),
            Tijdstip = DateTime.Now
        };

        // Geschiedenis ophalen, aanvullen, en opslaan
        var geschiedenis = Request.Cookies.GetObjectFromJson<List<OrderHistoryEntry>>("BestelGeschiedenis") ?? new();
        geschiedenis.Add(bestelling);
        Response.Cookies.SetObjectAsJson("BestelGeschiedenis", geschiedenis);

        // Winkelwagen legen
        Response.Cookies.Delete("Winkelwagen");

        // Succesmelding tonen op de Index pagina
        TempData["SuccessMessage"] = $"Bestelling succesvol geplaatst! <a href='/History'>Bekijk bestelgeschiedenis</a>.";

        return RedirectToPage("/Index");
    }

    public class Bestelformulier
    {
        [Required]
        public required string Naam { get; set; }

        [Required]
        public required string Achternaam { get; set; }

        [Required]
        public required string Adres { get; set; }

        [Required]
        public required string Woonplaats { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        [Phone]
        public required string Telefoonnummer { get; set; }

        [Required]
        public required string Betaalmethode { get; set; }
    }
}