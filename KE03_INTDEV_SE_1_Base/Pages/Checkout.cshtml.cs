using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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

        // Hier kun je de bestelling verwerken of opslaan  

        TempData["SuccessMessage"] = "Bestelling succesvol geplaatst!";
        return RedirectToPage("Index");
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
