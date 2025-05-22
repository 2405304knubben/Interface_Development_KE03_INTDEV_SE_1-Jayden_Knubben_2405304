using System.Security.Claims;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models;

public class LoginModel : PageModel
{
    private readonly MatrixIncDbContext _context;

    public LoginModel(MatrixIncDbContext context)
    {
        _context = context;
        Name = string.Empty;
        Password = string.Empty;
        Foutmelding = string.Empty;
    }

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string Foutmelding { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var Customer = _context.Customers
            .FirstOrDefault(c => c.Name == Name && c.Password == Password);

        if (Customer == null)
        {
            Foutmelding = "Ongeldige inloggegevens";
            return Page(); // blijf op de inlogpagina    
        }

        var claims = new List<Claim>
       {
           new Claim(ClaimTypes.Name, Customer.Name)
       };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //TempData["username"] = Name;

        if (string.IsNullOrEmpty(TempData["username"] as string))
        {
            Response.Redirect("/Log-in");
        }

        TempData.Keep("username");
        ViewData["Title"] = "Home page";
        return Redirect("~/Index");
    }
}