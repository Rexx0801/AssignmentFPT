using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NWEC.P.L001_Task3.DataAccessLayer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}