using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Panda.Domain
{
    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Packages = new List<Package>();
            this.Receipt = new List<Receipt>();
        }

        public PandaUserRole UserRole { get; set; }

        public List<Package> Packages { get; set; }

        public List<Receipt> Receipt { get; set; }
    }
}
