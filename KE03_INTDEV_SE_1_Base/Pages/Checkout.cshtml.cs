using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models; 
using System.Collections.Generic;
using System.Linq;
using System;

public class CheckoutModel : PageModel
{
    [BindProperty]
    public Bestelformulier Form { get; set; } = new()
    {
        Name = string.Empty,
        Surname = string.Empty,
        Address = string.Empty,
        Location = string.Empty,
        Email = string.Empty,
        Phonenumber = string.Empty,
        Paying_Method = string.Empty
    };

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Winkelwagen ophalen
        var shoppingcart = Request.Cookies.GetObjectFromJson<List<Shoppingcart>>("Winkelwagen") ?? new();

        if (!shoppingcart.Any())
        {
            TempData["SuccessMessage"] = "Geen producten in de winkelwagen.";
            return RedirectToPage("/Index");
        }

        // Nieuwe bestelling aanmaken
        var order = new OrderHistoryEntry
        {
            Name = Form.Name,
            Surname = Form.Surname,
            Address = Form.Address,
            Location = Form.Location,
            Email = Form.Email,
            Phonenumber = Form.Phonenumber,
            Paying_Method = Form.Paying_Method,
            OrderedItems = shoppingcart.Select(p => new Shoppingcart
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList(),
            Time = DateTime.Now
        };

        // Geschiedenis ophalen, aanvullen, en opslaan
        var history = Request.Cookies.GetObjectFromJson<List<OrderHistoryEntry>>("BestelGeschiedenis") ?? new();
        history.Add(order);
        Response.Cookies.SetObjectAsJson("BestelGeschiedenis", history);

        // Winkelwagen legen
        Response.Cookies.Delete("Winkelwagen");

        // Succesmelding tonen op de Index pagina
        TempData["SuccessMessage"] = $"Bestelling succesvol geplaatst!";

        return RedirectToPage("/Index");
    }

    public class Bestelformulier
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Surname { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string Location { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        [Phone]
        public required string Phonenumber { get; set; }

        [Required]
        public required string Paying_Method { get; set; }
    }
}