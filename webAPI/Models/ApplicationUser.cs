using Microsoft.AspNetCore.Identity;
using System;

namespace webAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
    }
}
