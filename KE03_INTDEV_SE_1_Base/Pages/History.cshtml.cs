using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using DataAccessLayer.Models; // Of waar Shoppingcart staat

public class HistoryModel : PageModel
{
    public List<OrderHistoryEntry> Bestellingen { get; set; } = new();

    public void OnGet()
    {
        Bestellingen = Request.Cookies.GetObjectFromJson<List<OrderHistoryEntry>>("BestelGeschiedenis") ?? new();
    }
}
