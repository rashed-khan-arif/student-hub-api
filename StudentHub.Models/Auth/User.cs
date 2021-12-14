using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace StudentHub.Models.Auth
{
    public class User: IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Sex { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
