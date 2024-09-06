using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public string? Nombre { get; set; }
    }
}
