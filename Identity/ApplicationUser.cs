using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VTYSProje.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}