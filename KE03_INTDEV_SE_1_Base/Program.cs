using System;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace KE03_INTDEV_SE_1_Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MatrixIncDbContext>(
                options => options.UseSqlite("Data Source=MatrixInc.db"));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MatrixIncDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<IdentityUser>>();
            builder.Services.AddScoped<SignInManager<IdentityUser>>();

            
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Log-in";
                options.AccessDeniedPath = "/AccessDenied";
            });

            
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Log-in"; 
                    options.AccessDeniedPath = "/AccessDenied";
                });

            // Repositories
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPartRepository, PartRepository>();

            // Razor Pages
            builder.Services.AddRazorPages();

            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession(); 

            app.MapRazorPages();

            app.MapGet("/", context =>
            {
                if (context.Request.Cookies.TryGetValue("auth_cookie", out var authValue) && authValue == "true")
                {
                    context.Response.Redirect("/Index");
                }
                else
                {
                    context.Response.Redirect("/Log-in");
                }
                context.Response.Cookies.Append("auth_cookie", "true", new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });
                return Task.CompletedTask;
            });

            app.MapGet("/logout", async (HttpContext context) =>
            {
                await Task.Run(() =>
                {
                    context.Response.Cookies.Delete("loginCookieNaam"); 
                    context.Response.Redirect("/Log-in"); 
                });
            });

            // Ensure database exists en seeded is
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MatrixIncDbContext>();
                db.Database.EnsureCreated();
                using (var scope1 = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<MatrixIncDbContext>();
                    try
                    {
                        db.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while creating the database: {ex.Message}");
                        throw;
                    }

                    if (!db.Products.Any())
                    {
                        db.Products.AddRange(
                            new Product { Name = "Laptop", Price = 999.99M, Description = "High-performance laptop", ImageUrl = "", ImageAuthor = "" },
                            new Product { Name = "Muis", Price = 25.50M, Description = "Ergonomic mouse", ImageUrl = "", ImageAuthor = "" },
                            new Product { Name = "Toetsenbord", Price = 45.00M, Description = "Mechanical keyboard", ImageUrl = "", ImageAuthor = "" }
                        );
                        db.SaveChanges();
                    }
                }
                if (!db.Products.Any())
                {
                    db.Products.AddRange(
                        new Product { Name = "Laptop", Price = 999.99M, Description = "High-performance laptop", ImageUrl = "", ImageAuthor = "" },
                        new Product { Name = "Muis", Price = 25.50M, Description = "Ergonomic mouse", ImageUrl = "", ImageAuthor = "" },
                        new Product { Name = "Toetsenbord", Price = 45.00M, Description = "Mechanical keyboard", ImageUrl = "", ImageAuthor = "" }
                    );
                    db.SaveChanges();
                }
            }

            app.Run();
        }
    }
}