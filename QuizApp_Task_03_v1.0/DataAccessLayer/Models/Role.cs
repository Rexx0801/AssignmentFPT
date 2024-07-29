using Microsoft.AspNetCore.Identity;
using System;

namespace NWEC.P.L001_Task3.DataAccessLayer.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}