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

    public IActionResult OnPostBuyProduct(int productId, int amount)
    {
        TempData["DebugMessage"] = $"Ontvangen productId: {productId}, aantal: {amount}";

        var shoppingbasket = Request.Cookies.GetObjectFromJson<List<Shoppingcart>>("Winkelwagen") ?? new();

        var exist = shoppingbasket.FirstOrDefault(p => p.ProductId == productId);
        if (exist != null)
        {
            exist.Quantity += amount;
            //return RedirectToPage();
            
        }
        else
        {
            var product = _context.Products.Find(productId); 
            if (product != null)
            {
                shoppingbasket.Add(new Shoppingcart
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = amount
                });
            }
        }

        Response.Cookies.SetObjectAsJson("Winkelwagen", shoppingbasket);
        TempData["SuccessMessage"] = "Product succesvol toegevoegd aan de winkelmand!";
        return RedirectToPage();
    }

    public IActionResult OnPostBuyPart(int productId)
    {
        var shoppingbasket = Request.Cookies.GetObjectFromJson<List<Shoppingcart>>("Winkelwagen") ?? new();

        var exist = shoppingbasket.FirstOrDefault(p => p.ProductId == productId && p.IsPart);
        if (exist != null)
        {
            exist.Quantity += 1;
        }
        else
        {
            var part = _context.Parts.Find(productId);
            if (part != null)
            {
                shoppingbasket.Add(new Shoppingcart
                {
                    ProductId = part.Id,
                    Name = part.Name,
                    Price = part.Price,
                    Quantity = 1,
                    IsPart = true
                });
            }
        }

        Response.Cookies.SetObjectAsJson("Winkelwagen", shoppingbasket);
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
