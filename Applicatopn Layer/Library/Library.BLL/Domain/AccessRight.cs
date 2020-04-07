using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL.Domain
{
    public class AccessRight : IdentityRole<int>
    {
        public string roleName { get; set; }
    }
}

