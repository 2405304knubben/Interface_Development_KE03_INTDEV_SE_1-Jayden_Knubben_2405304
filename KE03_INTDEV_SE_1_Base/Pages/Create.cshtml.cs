using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccessLayer.Repositories;
using Newtonsoft.Json;

public class CreateModel : PageModel
{
    private readonly MatrixIncDbContext _context;

    public CreateModel(MatrixIncDbContext context)
    {
        _context = context;
        Products = new List<Product>();
        Parts = new List<Part>();
    }

    public List<Product> Products { get; set; }
    public List<Part> Parts { get; set; }

    public void OnGet()
    {
        Products = _context.Products.ToList();
        Parts = _context.Parts.ToList();
    }

    public IActionResult OnPostKoopProduct(int productId, int aantal)
    {
        TempData["DebugMessage"] = $"Ontvangen productId: {productId}, aantal: {aantal}";

        var winkelmand = Request.Cookies.GetObjectFromJson<List<Shoppingcart>>("Winkelwagen") ?? new();

        var bestaand = winkelmand.FirstOrDefault(p => p.ProductId == productId);
        if (bestaand != null)
        {
            bestaand.Quantity += aantal;
            //TempData["ErrorMessage"] = $"Product niet gevonden voor ID: {productId}";
            return RedirectToPage();
            
        }
        else
        {
            // Voeg nieuw item toe. Zorg dat Shoppingcart alles bevat (zoals Name & Price)
            var product = _context.Products.Find(productId); // of hoe je je product ophaalt
            if (product != null)
            {
                winkelmand.Add(new Shoppingcart
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = aantal
                });
            }
        }

        Response.Cookies.SetObjectAsJson("Winkelwagen", winkelmand);
        TempData["SuccessMessage"] = "Product succesvol toegevoegd aan de winkelmand!";
        return RedirectToPage();
    }

    public IActionResult OnPostKoopPart(int productId)
    {
        var winkelmand = Request.Cookies.GetObjectFromJson<List<Shoppingcart>>("Winkelwagen") ?? new();

        var bestaand = winkelmand.FirstOrDefault(p => p.ProductId == productId && p.IsPart);
        if (bestaand != null)
        {
            bestaand.Quantity += 1;
        }
        else
        {
            var part = _context.Parts.Find(productId);
            if (part != null)
            {
                winkelmand.Add(new Shoppingcart
                {
                    ProductId = part.Id,
                    Name = part.Name,
                    Price = part.Price,
                    Quantity = 1,
                    IsPart = true
                });
            }
        }

        Response.Cookies.SetObjectAsJson("Winkelwagen", winkelmand);
        TempData["SuccessMessage"] = "Onderdeel succesvol toegevoegd aan winkelmand!";
        return RedirectToPage();
    }
    public IActionResult OnPostAddToCart(List<int> selectedProductIds)
    {
        var selectedProducts = _context.Products
                                       .Where(p => selectedProductIds.Contains(p.Id))
                                       .ToList();

        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(selectedProducts));
        TempData["Message"] = "Producten succesvol toegevoegd aan winkelmand.";
        return RedirectToPage("Create");
    }
}
