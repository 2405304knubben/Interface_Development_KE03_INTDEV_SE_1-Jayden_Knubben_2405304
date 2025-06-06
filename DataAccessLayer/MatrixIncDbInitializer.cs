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
                   new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m, ImageUrl = "https://www.hdwallpapers.in/thumbs/2020/the_matrix_2_hd_movies-t2.jpg", ImageAuthor = "Lana Wachowski en Lilly Wachowski, hdwallpapers.in"},
                   new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m, ImageUrl = "https://th.bing.com/th/id/R.2b81fcaecac5d30f3342b43cad743918?rik=qTiQ5phDlYOJ1Q&riu=http%3a%2f%2fi.dailymail.co.uk%2fi%2fpix%2f2008%2f05%2f30%2farticle-0-016E95D800000578-473_468x286.jpg&ehk=RNZUnXvbvIY%2bt%2ft%2bYGOuvIBXOJZpHS7sbPqORUx9Jf4%3d&risl=&pid=ImgRaw&r=0", ImageAuthor = "dailymail.co.uk" },
                   new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m, ImageUrl = "https://th.bing.com/th/id/R.dd299f10068fae09e897d80f3e6ad2d4?rik=gt2rmV1dE9MfGA&riu=http%3a%2f%2fimg2.wikia.nocookie.net%2f__cb20130130031910%2fmatrix%2fimages%2f1%2f14%2fEMP_Activator.png&ehk=SW1C1x0kcfxzWj5m9h6VPE6kwdMr3VJZED%2biltGsCRI%3d&risl=&pid=ImgRaw&r=0", ImageAuthor = "matrix.fandom.com" }
            };
            context.Products.AddRange(products);

            // Seed parts  
            var parts = new Part[]
            {
                   new Part { Name = "Tandwiel (x6)", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen", Price = 80.00m, ImageUrl = "https://media.istockphoto.com/id/988798508/photo/detail-of-gears-and-chains-parts-of-the-metal-crane-to-pull-the-boat-to-the-shore.jpg?b=1&s=612x612&w=0&k=20&c=XPcN3Y-yRXFO9hlfLo1gurdHw81-5qEGhmuYc1vhMuc=", ImageAuthor = "media.istockphoto.com" },
                   new Part { Name = "M5 Boutje (x105)", Description = "Bevestiging van panelen, buizen of interne modules", Price = 40.00m, ImageUrl = "https://jaakvanwijck.nl/webshop/wp-content/uploads/2019/04/RVSmetaalCKkruisnew-029.jpg", ImageAuthor = "jaakvanwijck.nl" },
                   new Part { Name = "Hydraulische cilinder (x2)", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen", Price = 295.00m, ImageUrl = "https://hytres.com/wp-content/uploads/cilinders_1-scaled.jpg", ImageAuthor = "hytres.com" },
                   new Part { Name = "Koelvloeistofpomp (x3)", Description = "Koeling van de motor of elektronische systemen.", Price = 242.00m, ImageUrl = "https://th.bing.com/th/id/OIP.Dc1F5c7CoG4ZdNrGhbbyGwHaHa?cb=iwc2&rs=1&pid=ImgDetMain", ImageAuthor = "techniekshop.nl" }
            };
            context.Parts.AddRange(parts);

            // Save changes to the database  
            context.SaveChanges();
        }
       }
}
