using System;

namespace DataAccessLayer.Models
{
    public class Login
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; } // Voor demo, géén hash (in productie altijd hashen!)
    }
}

