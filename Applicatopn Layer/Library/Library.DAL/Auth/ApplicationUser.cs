using Library.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Auth
{
    public class ApplicationUser : IdentityUser<int>
    {
        public AccessRight AccessRight { get; set; }
        public List<InstrumentEF> Instruments { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
