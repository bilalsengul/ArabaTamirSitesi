using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTYSProje.Identity;

namespace VTYSProje.Models
{
    public class RoleUpdata
    {

        public ApplicationRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}