using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL.Domain
{
    public class User : IdentityUser<int>
    {
        public AccessRight AccessRight { get; set; }
        public List<Instrument> Instruments { get; set; }
        public bool IsIndependance { get; set; }
        public bool IsGarde { get; set; }
    }
}
