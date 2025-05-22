using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class HistoryModel : PageModel
{
    private readonly MatrixIncDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HistoryModel(MatrixIncDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<OrderViewModel> Orders { get; set; } = new();

    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public required string Name { get; set; }
    }
}
