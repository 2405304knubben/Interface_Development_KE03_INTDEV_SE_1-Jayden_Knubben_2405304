using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models; 
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System;

public class CheckoutModel : PageModel
{
    private readonly MatrixIncDbContext _context;

    public CheckoutModel(MatrixIncDbContext context)
    {
        _context = context;
    }

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
            return Page();

        // Winkelwagen ophalen uit cookies
        var shoppingcart = Request.Cookies.GetObjectFromJson<List<Shoppingcart>>("Winkelwagen") ?? new();

        if (!shoppingcart.Any())
        {
            TempData["SuccessMessage"] = "Geen producten in de winkelwagen.";
            return RedirectToPage("/Index");
        }

        // Order aanmaken
        var order = new Order
        {
            Name = Form.Name,
            Surname = Form.Surname,
            Adress = Form.Address,
            Location = Form.Location,
            Email = Form.Email,
            Phonenumber = Form.Phonenumber,
            Paying_Method = Form.Paying_Method,
            OrderDate = DateTime.Now,
            OrderRegels = shoppingcart.Select(p => new OrderRegel
            {
                ProductId = p.ProductId,
                Quantity= p.Quantity,
                Price = p.Price,
                ProductName = p.ProductName,
            }).ToList()
        };

        _context.Orders.Add(order);
        _context.SaveChanges(); // Slaat bestelling + orderregels op in DB

        Response.Cookies.Delete("Winkelwagen");
        TempData["SuccessMessage"] = "Bestelling succesvol geplaatst!";
        return RedirectToPage("/Index");
    }

    public class Bestelformulier
    {
        [Required] public required string Name { get; set; }
        [Required] public required string Surname { get; set; }
        [Required] public required string Address { get; set; }
        [Required] public required string Location { get; set; }
        [Required, EmailAddress] public required string Email { get; set; }
        [Required, Phone] public required string Phonenumber { get; set; }
        [Required] public required string Paying_Method { get; set; }
    }
}