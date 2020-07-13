using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DAL
{
    public class AccessRight : IdentityRole<int>
    {
        public AccessRight() : base()
        { }
        public AccessRight(string roleName) : base(roleName)
        { }
    }
}

