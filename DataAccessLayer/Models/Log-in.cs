using System;

namespace DataAccessLayer.Models
{
    public class Login
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; } 
    }
}

