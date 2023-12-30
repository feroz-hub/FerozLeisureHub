using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FerozLeisureHub.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}