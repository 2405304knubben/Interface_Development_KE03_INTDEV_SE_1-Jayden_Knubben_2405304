using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Ensure database is created  
            context.Database.EnsureCreated();

            // Look for any customers.  
            if (context.Customers.Any())
            {
                return; // DB has been seeded  
            }

            // Seed customers  
            var customers = new Customer[]
            {
                   new Customer { Name = "Neo", Password = "Matrix123!", Address = "123 Elm St", Active = true },
                   new Customer { Name = "Morpheus", Password = "Matrix123!", Address = "456 Oak St", Active = true },
                   new Customer { Name = "Trinity", Password = "Matrix123!", Address = "789 Pine St", Active = true }
            };
            context.Customers.AddRange(customers);

            // Seed orders  
            var orders = new Order[]
            {
                   new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01"), Name = "Neo", Surname = "Matrix", Adress= "Maandagstraat 1", Location = "Heerlen", Email = "neothegod@gmail.com", Phonenumber = "06583940163", Paying_Method = "ABN Amro"  },
            };
            context.Orders.AddRange(orders);

            // Seed products  
            var products = new Product[]
            {
                   new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m, ImageSRC = "Images/Nebuchadnezzar.jpg", ImageAuthor = "Lana Wachowski en Lilly Wachowski, hdwallpapers.in"},
                   new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m, ImageSRC = "Images/Jack-In Chair.jpg", ImageAuthor = "dailymail.co.uk" },
                   new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m, ImageSRC = "Images/EMP-Device.jpg", ImageAuthor = "matrix.fandom.com" }
            };
            context.Products.AddRange(products);

            // Seed parts  
            var parts = new Part[]
            {
                   new Part { Name = "Tandwiel (x6)", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen", Price = 80.00m, ImageSRC = "Images/Tandwiel.jpg", ImageAuthor = "media.istockphoto.com" },
                   new Part { Name = "M5 Boutje (x105)", Description = "Bevestiging van panelen, buizen of interne modules", Price = 40.00m, ImageSRC = "Images/Boutjes.jpg", ImageAuthor = "jaakvanwijck.nl" },
                   new Part { Name = "Hydraulische cilinder (x2)", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen", Price = 295.00m, ImageSRC = "Images/Hydraulische cilinder.jpg", ImageAuthor = "hytres.com" },
                   new Part { Name = "Koelvloeistofpomp (x3)", Description = "Koeling van de motor of elektronische systemen.", Price = 242.00m, ImageSRC = "Images/Koelvloeistofpomp.jpg", ImageAuthor = "techniekshop.nl" }
            };
            context.Parts.AddRange(parts);

            // Save changes to the database  
            context.SaveChanges();
        }
       }
}
