using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

public class OrderModel : PageModel
{
    public List<Shoppingcart> Items { get; set; } = new();

    public void OnGet()
    {
        Items = GetCartItemsFromCookie();
    }

    public List<OrderItem> CartItems { get; set; } = new();

    public class OrderItem
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public IActionResult OnPostUpdateQuantity(int productId, string action)
    {
        var winkelmand = GetCartItemsFromCookie();

        var item = winkelmand.FirstOrDefault(p => p.ProductId == productId);
        if (item != null)
        {
            if (action == "increase")
            {
                item.Quantity++;
            }
            else if (action == "decrease" && item.Quantity > 1)
            {
                item.Quantity--;
            }

            SaveCartItemsToCookie(winkelmand);
        }

        return RedirectToPage();
    }

    public IActionResult OnPostRemoveItem(int productId)
    {
        var items = GetCartItemsFromCookie();

        var itemToRemove = items.FirstOrDefault(i => i.ProductId == productId);
        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            SaveCartItemsToCookie(items);
        }

        TempData["SuccessMessage"] = "Item verwijderd uit de winkelwagen.";
        return RedirectToPage();
    }

    // Helper methods
    private List<Shoppingcart> GetCartItemsFromCookie()
    {
        var cookie = Request.Cookies["Winkelwagen"];
        return string.IsNullOrEmpty(cookie)
            ? new List<Shoppingcart>()
            : JsonSerializer.Deserialize<List<Shoppingcart>>(cookie) ?? new List<Shoppingcart>();
    }

    private void SaveCartItemsToCookie(List<Shoppingcart> items)
    {
        var options = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(7),
            HttpOnly = true,
            Secure = true, // alleen via HTTPS
            IsEssential = true
        };

        Response.Cookies.Append("Winkelwagen", JsonSerializer.Serialize(items), options);
    }
}