using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class HistoryModel : PageModel
{
    private readonly MatrixIncDbContext _context;

    public HistoryModel(MatrixIncDbContext context)
    {
        _context = context;
    }

    public List<Order> Orders { get; set; } = new();

    public void OnGet()
    {
        Orders = _context.Orders
            .Where(o => o.OrderRegels != null)
            .Include(o => o.OrderRegels)
                .ThenInclude(r => r.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
    }
}
