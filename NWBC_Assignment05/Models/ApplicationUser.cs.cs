﻿using Microsoft.AspNetCore.Identity;
namespace NWBC_Assignment03.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
