﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Security
{
    public class AuthResponse
    {
        public string IdUser { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
