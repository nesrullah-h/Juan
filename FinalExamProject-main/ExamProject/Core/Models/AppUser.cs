using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class AppUser:IdentityUser
    {
        public bool IsActivated { get; set; }
    }
}
