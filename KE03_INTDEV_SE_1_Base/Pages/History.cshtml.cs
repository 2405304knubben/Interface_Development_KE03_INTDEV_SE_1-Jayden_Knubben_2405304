using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using DataAccessLayer.Models;

public class HistoryModel : PageModel
{
    public List<OrderHistoryEntry> Orders { get; set; } = new();

    public void OnGet()
    {
        Orders = Request.Cookies.GetObjectFromJson<List<OrderHistoryEntry>>("BestelGeschiedenis") ?? new();  
    }
}
